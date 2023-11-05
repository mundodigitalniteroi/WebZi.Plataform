using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Empresa;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Veiculo;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Vistoria;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GGV;
using WebZi.Plataform.Domain.ViewModel.GGV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.Veiculo;
using WebZi.Plataform.Domain.Views.Faturamento;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebZi.Plataform.Data.Services.GGV
{
    public class GgvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public GgvService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MensagemViewModel> Cadastrar(GgvPersistenciaViewModel GrvPersistencia)
        {
            MensagemViewModel ResultView = await ValidarInformacoesPersistenciaAsync(GrvPersistencia);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(x => x.GrvId == GrvPersistencia.IdentificadorGrv)
                .FirstOrDefaultAsync();

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);

            Grv.UsuarioCadastroGgvId = GrvPersistencia.IdentificadorUsuario;
            Grv.DataAlteracao = DataHoraPorDeposito;
            Grv.DataHoraGuarda = GrvPersistencia.DataHoraGuarda;
            Grv.FlagChaveDeposito = GrvPersistencia.FlagChaveDeposito;

            if (GrvPersistencia.FlagChaveDeposito == "S")
            {
                Grv.NumeroChave = GrvPersistencia.NumeroChave;
            }

            Grv.EstacionamentoSetor = GrvPersistencia.EstacionamentoSetor;

            Grv.EstacionamentoNumeroVaga = GrvPersistencia.EstacionamentoNumeroVaga;

            if (GrvPersistencia.FlagTransbordo == "S")
            {
                Grv.FlagTransbordo = "S";

                Grv.DataTransbordo = GrvPersistencia.DataTransbordo;
            }

            // TODO:
            // CadastrarEquipamentos()

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (GrvPersistencia.Vistoria != null)
                    {
                        VistoriaModel Vistoria = new();

                        Grv.Vistoria = Vistoria;
                    }

                    _context.Grv.Update(Grv);

                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return MensagemViewHelper.GetInternalServerError(ex);
                }
            }

            if (GrvPersistencia.Fotos?.Count > 0)
            {
                List<BucketFileModel> Files = new();

                foreach (CadastroFotoTipoCadastroViewModel item in GrvPersistencia.Fotos)
                {
                    Files.Add(new BucketFileModel
                    {
                        TipoCadastroId = item.IdentificadorTipoCadastro,
                        File = item.Foto
                    });
                }

                new BucketArquivoService(_context, _httpClientFactory)
                    .SendFiles("GGVFOTOSVEICCAD",
                        GrvPersistencia.IdentificadorGrv,
                        GrvPersistencia.IdentificadorUsuario,
                        Files);
            }

            return MensagemViewHelper.GetOkCreate(1);
        }

        public MensagemViewModel CadastrarFotos(CadastroFotoGgvViewModel Fotos)
        {
            MensagemViewModel ResultView = new GrvService(_context)
                .ValidarInputGrv(Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Fotos.Listagem?.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = new GrvService(_context).GetGrv(Fotos.IdentificadorGrv);

            if (!new[] { "V", "L", "U", "T", "R", "E", "B", "D", "1", "2", "3", "4" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O Status da Operação deste GRV não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<BucketFileModel> Files = new();

            foreach (CadastroFotoTipoCadastroViewModel item in Fotos.Listagem)
            {
                Files.Add(new BucketFileModel { TipoCadastroId = item.IdentificadorTipoCadastro, File = item.Foto });
            }

            new BucketArquivoService(_context, _httpClientFactory)
                .SendFiles("GGVFOTOSVEICCAD", Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario, Files);

            return MensagemViewHelper.GetOkCreate(Fotos.Listagem.Count);
        }

        public async Task<MensagemViewModel> ExcluirFotosAsync(int GrvId, int UsuarioId, List<int> ListagemTabelaOrigemId)
        {
            if (ListagemTabelaOrigemId.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Informe os Identificadores das Fotos");
            }

            MensagemViewModel ResultView = new GrvService(_context).ValidarInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await new GrvService(_context).GetGrvAsync(GrvId);

            if (!new[] { "E", "B", "D", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O GRV está em um Status de Operação que impede a exclusão de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<BucketArquivoModel> BucketArquivos = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId != GrvId
                         && ListagemTabelaOrigemId.Contains(x.RepositorioArquivoId)
                         && x.BucketNomeTabelaOrigem.Codigo == "GGVFOTOSVEICCAD")
                .AsNoTracking()
                .ToListAsync();

            if (BucketArquivos?.Count > 0)
            {
                List<string> erros = new()
                {
                    $"A(s) seguinte(s) Fotos não pertencem ao GRV {GrvId}:"
                };

                foreach (BucketArquivoModel BucketArquivo in BucketArquivos)
                {
                    erros.Add(BucketArquivo.RepositorioArquivoId.ToString());
                }

                return MensagemViewHelper.GetBadRequest(erros);
            }

            new BucketArquivoService(_context, _httpClientFactory)
                .DeleteFiles("GGVFOTOSVEICCAD", ListagemTabelaOrigemId);

            return MensagemViewHelper.GetOkDelete(BucketArquivos.Count, "Foto(s) excluída(s) com sucesso");
        }

        public async Task<DadosMestresViewModel> ListarDadosMestresAsync()
        {
            VistoriaService VistoriaService = new(_context, _mapper);

            SistemaService SistemaService = new(_context, _mapper);

            DadosMestresViewModel DadosMestres = new()
            {
                Mensagem = MensagemViewHelper.GetOk(),

                ListagemEmpresa = await new EmpresaService(_context, _mapper)
                    .ListAsync(),

                ListagemCorOstentada = await new SistemaService(_context, _mapper)
                    .ListarCorAsync(),

                ListagemEstadoGeralVeiculo = await SistemaService
                    .ListarViewTabelaGenericaAsync("VISTORIA_ESTADO_GERAL_VEICULO"),

                ListagemSituacaoChassi = await VistoriaService
                    .ListarSituacaoChassiAsync(),

                ListagemStatusVistoria = await VistoriaService
                    .ListarStatusVistoriaAsync(),

                ListagemTipoAvaria = await new TipoAvariaService(_context, _mapper)
                    .ListarTipoAvariaAsync(),

                ListagemTipoCadastroFotoGGV = await SistemaService
                    .ListarViewTabelaGenericaAsync("GGV_TIPO_CADASTRO_FOTO"),

                ListagemTipoDirecao = await SistemaService
                    .ListarViewTabelaGenericaAsync("VISTORIA_TIPO_DIRECAO")
            };

            return DadosMestres;
        }

        public async Task<ServicoAssociadoTipoVeiculoViewModelList> ListarServicoAssociadoTipoVeiculoAsync(int IdentificadorGrv, int IdentificadorUsuario)
        {
            ServicoAssociadoTipoVeiculoViewModelList ListagemServicoAssociadoTipoVeiculo = new();

            MensagemViewModel Mensagem = new GrvService(_context)
                .ValidarInputGrv(IdentificadorGrv, IdentificadorUsuario);

            if (Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                ListagemServicoAssociadoTipoVeiculo.Mensagem = Mensagem;

                return ListagemServicoAssociadoTipoVeiculo;
            }

            GrvModel Grv = await _context.Grv
                .Where(x => x.GrvId == IdentificadorGrv)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            List<ViewFaturamentoServicoAssociadoVeiculoModel> ListagemFaturamentoServicoAssociadoTipoVeiculo = _context
                .ViewFaturamentoServicoAssociadoVeiculo
                .Where(w => w.ClienteId == Grv.ClienteId &&
                            w.DepositoId == Grv.DepositoId &&
                            w.TipoVeiculoId == Grv.TipoVeiculoId &&
                            w.FaturamentoProdutoId == Grv.FaturamentoProdutoId &&
                            (new[] { "DEP", "DRF" }.Contains(Grv.FaturamentoProdutoId) ? w.FlagCobrarGgv == "S" : true))
                .AsNoTracking()
                .ToList();

            if (ListagemFaturamentoServicoAssociadoTipoVeiculo?.Count > 0)
            {
                foreach (var item in ListagemFaturamentoServicoAssociadoTipoVeiculo)
                {
                    if (item.FlagNaoCobrarSeNaoUsouReboque == "N" && Grv.FlagComboio == "S")
                    {
                        continue;
                    }
                    else if (item.FlagServicoObrigatorio == "S" || item.FlagServicoObrigatorioGlobal == "S")
                    {
                        continue;
                    }

                    ListagemServicoAssociadoTipoVeiculo.Listagem.Add(new()
                    {
                        FaturamentoServicoTipoVeiculoId = item.FaturamentoServicoTipoVeiculoId,

                        ServicoDescricao = item.ServicoDescricao,

                        TipoCobranca = item.TipoCobranca,

                        FlagPermiteAlteracaoValor = item.FlagPermiteAlteracaoValor,

                        PrecoPadrao = item.PrecoPadrao,

                        PrecoValorMinimo = item.PrecoValorMinimo,

                        DataVigenciaInicial = item.DataVigenciaInicial
                    });
                }

                if (ListagemServicoAssociadoTipoVeiculo.Listagem.Count > 0)
                {
                    ListagemServicoAssociadoTipoVeiculo.Mensagem = MensagemViewHelper
                        .GetOkFound(ListagemServicoAssociadoTipoVeiculo.Listagem.Count);
                }
                else
                {
                    ListagemServicoAssociadoTipoVeiculo.Mensagem = MensagemViewHelper.GetNotFound();
                }
            }
            else
            {
                ListagemServicoAssociadoTipoVeiculo.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ListagemServicoAssociadoTipoVeiculo;
        }

        public async Task<ImageViewModelList> ListarFotosAsync(int GrvId, int UsuarioId)
        {
            ImageViewModelList ResultView = new()
            {
                Mensagem = new GrvService(_context).ValidarInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await new BucketArquivoService(_context, _httpClientFactory)
                .DownloadFiles("GGVFOTOSVEICCAD", GrvId);
        }

        public async Task<MensagemViewModel> ValidarInformacoesPersistenciaAsync(GgvPersistenciaViewModel GrvPersistencia)
        {
            if (GrvPersistencia == null)
            {
                return MensagemViewHelper.GetBadRequest("O Modelo está nulo");
            }

            MensagemViewModel ResultView = new GrvService(_context)
                .ValidarInputGrv(GrvPersistencia.IdentificadorGrv, GrvPersistencia.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            List<string> erros = new();

            GrvModel Grv = await _context.Grv
                .Include(x => x.Deposito)
                .Where(x => x.GrvId == GrvPersistencia.IdentificadorGrv)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);

            if (GrvPersistencia.DataHoraGuarda.Date > DataHoraPorDeposito.Date)
            {
                erros.Add("A Data da Guarda não pode ser maior do que a Data atual");
            }

            if (GrvPersistencia.DataHoraGuarda.Hour == 0 && GrvPersistencia.DataHoraGuarda.Minute == 0)
            {
                erros.Add("A Hora da Guarda não pode ser igual a 00:00");
            }

            if (Grv.DataHoraRemocao > GrvPersistencia.DataHoraGuarda)
            {
                erros.Add("A Data/Hora da Guarda não pode ser maior à Data/Hora da Remoção");
            }

            if (Grv.DataHoraRemocao == GrvPersistencia.DataHoraGuarda)
            {
                erros.Add("A Data/Hora da Guarda não pode ser igual à Data/Hora da Remoção");
            }

            if (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda == 0)
            {
                Grv.Deposito.GrvLimiteMinimoDatahoraGuarda = 20; // Anos
            }

            if (((DataHoraPorDeposito.Date - GrvPersistencia.DataHoraGuarda.Date).TotalDays) > (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda / 365))
            {
                if (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda == 1)
                {
                    erros.Add("A Data da Guarda não pode ser inferior a 1 ano.");
                }
                else
                {
                    erros.Add("A Data da Guarda não pode ser inferior a " + Grv.Deposito.GrvLimiteMinimoDatahoraGuarda + " anos.");
                }
            }

            if (GrvPersistencia.FlagChaveDeposito != "S" && GrvPersistencia.FlagChaveDeposito != "N")
            {
                erros.Add("Flag da Chave deixada no Depósito inválida, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvPersistencia.FlagChaveDeposito == "S" && string.IsNullOrWhiteSpace(GrvPersistencia.NumeroChave))
            {
                erros.Add("Informe o Número da Chave do Veículo");
            }

            if (GrvPersistencia.FlagTransbordo != "S" && GrvPersistencia.FlagTransbordo != "N")
            {
                erros.Add("Flag do Transbordo inválida, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvPersistencia.FlagTransbordo == "S")
            {
                if (!GrvPersistencia.DataTransbordo.HasValue)
                {
                    erros.Add("Data do Transbordo inválida");
                }
                else if (GrvPersistencia.DataTransbordo.Value > DataHoraPorDeposito)
                {
                    erros.Add("Data do Transbordo não pode ser maior do que a Data/Hora atual");
                }
            }

            if (true)
            {

            }

            if (Grv.Deposito.GrvMinimoFotosExigidas > 0)
            {
                if (GrvPersistencia.Fotos?.Count == 0)
                {
                    erros.Add("É necessário enviar pelo menos 1 Foto do Veículo");
                }
                else
                {
                    if (Grv.Deposito.GrvMinimoFotosExigidas > GrvPersistencia.Fotos.Count)
                    {
                        erros.Add($"É necessário enviar pelo menos {Grv.Deposito.GrvMinimoFotosExigidas} Fotos do Veículo");
                    }

                    int count = GrvPersistencia.Fotos
                        .Where(x => x.IdentificadorTipoCadastro <= 0)
                        .Count();

                    if (count == 1)
                    {
                        erros.Add($"Foi indentificado um Identificador do Tipo do Cadastro da Foto inválido");
                    }
                    else if (count > 1)
                    {
                        erros.Add($"Foram indentificados {count} Identificador do Tipo do Cadastro da Foto inválido");
                    }
                }
            }

            if (GrvPersistencia.ListagemEquipamentoOpcional?.Count > 0)
            {
                int count = GrvPersistencia.ListagemEquipamentoOpcional
                    .Where(x => x.IdentificadorEquipamentoOpcional <= 0)
                    .Count();

                if (count == 1)
                {
                    erros.Add($"Foi indentificado um Identificador do Equipamento Opcional inválido");
                }
                else if (count > 1)
                {
                    erros.Add($"Foram indentificados {count} Identificador do Equipamento Opcional inválido");
                }

                count = GrvPersistencia.ListagemEquipamentoOpcional
                    .Where(x => x.FlagEquipamentoAvariado != "S" && x.FlagEquipamentoAvariado != "N")
                    .Count();

                if (count == 1)
                {
                    erros.Add($"Foi indentificado uma Flag de Equipamento Avariado inválida, informe \"S\" ou \"N\" (sem aspas)");
                }
                else if (count > 1)
                {
                    erros.Add($"Foram indentificados {count} Flags de Equipamento Avariado inválida, informe \"S\" ou \"N\" (sem aspas)");
                }

                count = GrvPersistencia.ListagemEquipamentoOpcional
                    .Where(x => x.FlagEquipamentoAvariado == "S" && x.IdentificadorTipoAvariaId <= 0)
                    .Count();

                if (count == 1)
                {
                    erros.Add($"Foi indentificado um Identificador do Tipo da Avaria inválido");
                }
                else if (count > 1)
                {
                    erros.Add($"Foram indentificados {count} Identificador do Tipo da Avaria inválido");
                }
            }

            if (GrvPersistencia.Vistoria != null)
            {
                if (GrvPersistencia.Vistoria.FlagVistoria != "S" && GrvPersistencia.Vistoria.FlagVistoria != "N")
                {
                    erros.Add("Flag da realização da Vistoria inválida, informe \"S\" ou \"N\" (sem aspas)");
                }
                else if (GrvPersistencia.Vistoria.FlagVistoria == "S")
                {
                    if (GrvPersistencia.Vistoria.IdentificadorEmpresaVistoria > 0)
                    {
                        if (await _context.Empresa
                            .Where(w => w.EmpresaId == GrvPersistencia.Vistoria.IdentificadorEmpresaVistoria)
                            .AsNoTracking()
                            .FirstOrDefaultAsync() == null)
                        {
                            erros.Add("Identificador da Empresa inexistente");
                        }
                    }

                    if (GrvPersistencia.Vistoria.IdentificadorStatusVistoria > 0)
                    {
                        if (await _context.VistoriaStatus
                            .Where(x => x.VistoriaStatusId == GrvPersistencia.Vistoria.IdentificadorStatusVistoria)
                            .AsNoTracking()
                            .FirstOrDefaultAsync() == null)
                        {
                            erros.Add("Identificador do Status da Vistoria inexistente");
                        }
                    }

                    if (GrvPersistencia.Vistoria.IdentificadorSituacaoChassi <= 0)
                    {
                        erros.Add("Identificador da Situação do Chassi inválido");
                    }
                    else if (await _context.VistoriaSituacaoChassi
                        .Where(x => x.VistoriaSituacaoChassiId == GrvPersistencia.Vistoria.IdentificadorSituacaoChassi)
                        .AsNoTracking()
                        .FirstOrDefaultAsync() == null)
                    {
                        erros.Add("Identificador da Situação do Chassi inexistente");
                    }

                    if (GrvPersistencia.Vistoria.IdentificadorTipoDirecao > 0)
                    {
                        if (await _context.TabelaGenerica
                            .Where(x => x.Codigo == "VISTORIA_TIPO_DIRECAO"
                                && x.TabelaGenericaId == GrvPersistencia.Vistoria.IdentificadorTipoDirecao)
                            .AsNoTracking()
                            .FirstOrDefaultAsync() == null)
                        {
                            erros.Add("Identificador do Tipo de Direção inexistente");
                        }
                    }

                    if (GrvPersistencia.Vistoria.IdentificadorEstadoGeralVeiculo <= 0)
                    {
                        erros.Add("Identificador do Estado Geral do Veículo inválido");
                    }
                    else
                    {
                        if (await _context.TabelaGenerica
                            .Where(x => x.Codigo == "VISTORIA_ESTADO_GERAL_VEICULO"
                                && x.TabelaGenericaId == GrvPersistencia.Vistoria.IdentificadorEstadoGeralVeiculo)
                            .AsNoTracking()
                            .FirstOrDefaultAsync() == null)
                        {
                            erros.Add("Identificador do Estado Geral do Veículo inexistente");
                        }
                    }

                    if (GrvPersistencia.Vistoria.DataVistoria > DataHoraPorDeposito)
                    {
                        erros.Add("A Data da Vistoria não pode ser maior do que a Data Atual");
                    }

                    if (GrvPersistencia.Vistoria.FlagPossuiRestricoes != "S" && GrvPersistencia.Vistoria.FlagPossuiRestricoes != "N")
                    {
                        erros.Add("Flag Possui Restrições inválida, informe \"S\" ou \"N\" (sem aspas)");
                    }

                    if (GrvPersistencia.Vistoria.FlagPossuiVidroEletrico != "S" && GrvPersistencia.Vistoria.FlagPossuiVidroEletrico != "N")
                    {
                        erros.Add("Flag Possui Vidro Elétrico inválida, informe \"S\" ou \"N\" (sem aspas)");
                    }

                    if (GrvPersistencia.Vistoria.FlagPossuiTravaEletrica != "S" && GrvPersistencia.Vistoria.FlagPossuiTravaEletrica != "N")
                    {
                        erros.Add("Flag Possui Trava Elétrica inválida, informe \"S\" ou \"N\" (sem aspas)");
                    }

                    if (GrvPersistencia.Vistoria.FlagPossuiPlaca != "S" && GrvPersistencia.Vistoria.FlagPossuiPlaca != "N")
                    {
                        erros.Add("Flag Possui Placa inválida, informe \"S\" ou \"N\" (sem aspas)");
                    }
                    else if (GrvPersistencia.Vistoria.FlagPossuiPlaca == "S")
                    {
                        if (string.IsNullOrWhiteSpace(GrvPersistencia.Vistoria.PlacaOstentada))
                        {
                            erros.Add("Informe a Placa Ostentada");
                        }
                        else if (!VeiculoHelper.IsPlaca(GrvPersistencia.Vistoria.PlacaOstentada))
                        {
                            erros.Add("Placa Ostentada inválida");
                        }
                    }
                }
            }

            return erros.Count == 0 ? MensagemViewHelper.GetOk() : MensagemViewHelper.GetBadRequest(erros);
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Empresa;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Models.Vistoria;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GGV;
using WebZi.Plataform.Domain.ViewModel.GGV.Cadastro;

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

        public async Task<MensagemViewModel> CreateGgvAsync(CadastroGgvViewModel GgvPersistencia)
        {
            MensagemViewModel ResultView = await ValidarInformacoesPersistenciaAsync(GgvPersistencia);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .FirstOrDefaultAsync(x => x.GrvId == GgvPersistencia.IdentificadorGrv);

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);

            Grv.StatusOperacaoId = "V";
            Grv.UsuarioCadastroGgvId = GgvPersistencia.IdentificadorUsuario;
            Grv.DataAlteracao = DataHoraPorDeposito;
            Grv.DataHoraGuarda = GgvPersistencia.DataHoraGuarda;
            Grv.FlagChaveDeposito = GgvPersistencia.FlagChaveDeposito;

            if (GgvPersistencia.FlagChaveDeposito == "S")
            {
                Grv.NumeroChave = GgvPersistencia.NumeroChave;
            }

            Grv.EstacionamentoSetor = GgvPersistencia.EstacionamentoSetor;

            Grv.EstacionamentoNumeroVaga = GgvPersistencia.EstacionamentoNumeroVaga;

            if (GgvPersistencia.FlagTransbordo == "S")
            {
                Grv.FlagTransbordo = "S";

                Grv.DataTransbordo = GgvPersistencia.DataTransbordo;
            }

            List<CondutorEquipamentoOpcionalModel> ListagemCadastroCondutorEquipamentoOpcional = new();

            if (GgvPersistencia.ListagemEquipamentoOpcional?.Count > 0)
            {
                List<decimal> EquipamentoOpcionalIds = GgvPersistencia.ListagemEquipamentoOpcional
                    .Select(x => x.IdentificadorEquipamentoOpcional)
                    .Distinct()
                    .ToList();

                List<CondutorEquipamentoOpcionalModel> ListagemCondutorEquipamentoOpcional = await _context.CondutorEquipamentoOpcional
                    .Where(x => EquipamentoOpcionalIds.Contains(x.EquipamentoOpcionalId) && x.GrvId == Grv.GrvId)
                    .AsNoTracking()
                    .ToListAsync();

                Grv.ListagemCondutorEquipamentoOpcional = new HashSet<CondutorEquipamentoOpcionalModel>();

                CondutorEquipamentoOpcionalModel CadastroCondutorEquipamentoOpcional = new();

                CondutorEquipamentoOpcionalModel CondutorEquipamentoOpcional = new();

                List<TipoAvariaModel> ListagemTipoAvaria = await _context.TipoAvaria
                    .AsNoTracking()
                    .ToListAsync();

                foreach (var item in GgvPersistencia.ListagemEquipamentoOpcional)
                {
                    CadastroCondutorEquipamentoOpcional = new()
                    {
                        GrvId = Grv.GrvId,

                        EquipamentoOpcionalId = item.IdentificadorEquipamentoOpcional,

                        FlagPossuiEquipamento = item.FlagPossuiEquipamento
                    };

                    CondutorEquipamentoOpcional = ListagemCondutorEquipamentoOpcional
                        .FirstOrDefault(x => x.EquipamentoOpcionalId == item.IdentificadorEquipamentoOpcional);

                    // Já possui cadastro
                    if (CondutorEquipamentoOpcional != null)
                    {
                        if (CondutorEquipamentoOpcional.FlagPossuiEquipamento == item.FlagPossuiEquipamento
                         && (CondutorEquipamentoOpcional.FlagEquipamentoAvariado == item.FlagEquipamentoAvariado && CondutorEquipamentoOpcional.CodigoAvaria == item.IdentificadorTipoAvaria))
                        {
                            continue;
                        }

                        CadastroCondutorEquipamentoOpcional.CondutorEquipamentoOpcionalId = CondutorEquipamentoOpcional.CondutorEquipamentoOpcionalId;

                        CadastroCondutorEquipamentoOpcional.UsuarioAlteracaoId = GgvPersistencia.IdentificadorUsuario;

                        CadastroCondutorEquipamentoOpcional.DataAtualizacao = DataHoraPorDeposito;
                    }
                    else
                    {
                        CadastroCondutorEquipamentoOpcional = new()
                        {
                            GrvId = Grv.GrvId,

                            EquipamentoOpcionalId = item.IdentificadorEquipamentoOpcional,

                            FlagPossuiEquipamento = item.FlagPossuiEquipamento,

                            UsuarioCadastroId = GgvPersistencia.IdentificadorUsuario
                        };
                    }

                    if (item.FlagPossuiEquipamento == "S")
                    {
                        CadastroCondutorEquipamentoOpcional.FlagEquipamentoAvariado = item.FlagEquipamentoAvariado;

                        if (item.FlagEquipamentoAvariado == "S")
                        {
                            CadastroCondutorEquipamentoOpcional.CodigoAvaria = item.IdentificadorTipoAvaria;
                        }
                    }
                    else
                    {
                        CadastroCondutorEquipamentoOpcional.FlagEquipamentoAvariado = null;

                        CadastroCondutorEquipamentoOpcional.CodigoAvaria = null;
                    }

                    ListagemCadastroCondutorEquipamentoOpcional.Add(CadastroCondutorEquipamentoOpcional);
                }
            }

            VistoriaModel Vistoria = new();

            TabelaGenericaService TabelaGenericaService = new(_context);

            if (GgvPersistencia.Vistoria != null)
            {
                Vistoria.UsuarioCadastroId = GgvPersistencia.IdentificadorUsuario;

                if (GgvPersistencia.Vistoria.FlagVistoria == "N")
                {
                    if (!string.IsNullOrWhiteSpace(GgvPersistencia.Vistoria.MotivoNaoRealizacaoVistoria))
                    {
                        Vistoria.MotivoNaoRealizacaoVistoria = GgvPersistencia.Vistoria.MotivoNaoRealizacaoVistoria;
                    }
                    else
                    {
                        Vistoria.MotivoNaoRealizacaoVistoria = "VISTORIA NÃO REALIZADA";
                    }
                }
                else
                {
                    Grv.FlagVistoria = "S";

                    Vistoria = new()
                    {
                        UsuarioCadastroId = GgvPersistencia.IdentificadorUsuario,

                        MotivoNaoRealizacaoVistoria = null,

                        FlagPossuiRestricoes = GgvPersistencia.Vistoria.FlagPossuiRestricoes,

                        FlagPossuiVidroEletrico = GgvPersistencia.Vistoria.FlagPossuiVidroEletrico,

                        FlagPossuiTravaEletrica = GgvPersistencia.Vistoria.FlagPossuiTravaEletrica,

                        FlagPossuiPlaca = GgvPersistencia.Vistoria.FlagPossuiPlaca,

                        EmpresaVistoriaId = GgvPersistencia.Vistoria.IdentificadorEmpresaVistoria,

                        NumeroVistoria = GgvPersistencia.Vistoria.NumeroVistoria.ToUpperTrim().ToNullIfEmpty(),

                        NomeVistoriador = GgvPersistencia.Vistoria.NomeVistoriador.ToUpperTrim().ToNullIfEmpty(),

                        NumeroMotor = GgvPersistencia.Vistoria.NumeroMotor.ToUpperTrim().ToNullIfEmpty(),

                        DataVistoria = GgvPersistencia.Vistoria.DataVistoria,

                        ResumoVistoria = GgvPersistencia.Vistoria.ResumoVistoria.ToUpperTrim().ToNullIfEmpty(),

                        VistoriaStatusId = _context.VistoriaStatus
                            .AsNoTracking()
                            .FirstOrDefault(x => x.VistoriaStatusId == (byte)GgvPersistencia.Vistoria.IdentificadorStatusVistoria)
                            .VistoriaStatusId,

                        VistoriaSituacaoChassiId = _context.VistoriaSituacaoChassi
                            .AsNoTracking()
                            .FirstOrDefault(x => x.VistoriaSituacaoChassiId == GgvPersistencia.Vistoria.IdentificadorSituacaoChassi)
                            .VistoriaSituacaoChassiId,

                        // VISTORIA_TIPO_DIRECAO
                        TipoDirecao = await TabelaGenericaService.GetValorCadastroAsync(GgvPersistencia.Vistoria.IdentificadorTipoDirecao),

                        // VISTORIA_ESTADO_GERAL_VEICULO
                        EstadoGeralVeiculo = await TabelaGenericaService.GetValorCadastroAsync(GgvPersistencia.Vistoria.IdentificadorEstadoGeralVeiculo)
                    };

                    if (GgvPersistencia.Vistoria.FlagPossuiPlaca == "S")
                    {
                        Grv.PlacaOstentada = GgvPersistencia.Vistoria.PlacaOstentada;

                        Grv.CorOstentadaId = GgvPersistencia.Vistoria.IdentificadorCorOstentada;
                    }
                }

                Grv.Vistoria = Vistoria;
            }
            else
            {
                Vistoria = null;
            }

            if (GgvPersistencia.ListagemFaturamentoServicoGrv?.Count > 0)
            {
                Grv.ListagemFaturamentoServicoGrv = new HashSet<FaturamentoServicoGrvModel>();

                List<int> ids = GgvPersistencia.ListagemFaturamentoServicoGrv
                    .Where(x => x.IdentificadorServicoAssociadoTipoVeiculo > 0)
                    .Select(x => x.IdentificadorServicoAssociadoTipoVeiculo)
                    .Distinct()
                    .ToList();

                List<FaturamentoServicoTipoVeiculoModel> FaturamentoServicoTipoVeiculoList = await _context.FaturamentoServicoTipoVeiculo
                    .Include(x => x.FaturamentoServicoAssociado)
                    .ThenInclude(x => x.FaturamentoServicoTipo)
                    .Where(x => ids.Contains(x.FaturamentoServicoTipoVeiculoId))
                    .AsNoTracking()
                    .ToListAsync();

                FaturamentoServicoTipoVeiculoModel FaturamentoServicoTipoVeiculo = new();

                FaturamentoServicoGrvModel FaturamentoServicoGrv = new();

                foreach (var item in GgvPersistencia.ListagemFaturamentoServicoGrv)
                {
                    FaturamentoServicoGrv = new()
                    {
                        GrvId = GgvPersistencia.IdentificadorGrv,

                        FaturamentoServicoTipoVeiculoId = item.IdentificadorServicoAssociadoTipoVeiculo
                    };

                    FaturamentoServicoTipoVeiculo = FaturamentoServicoTipoVeiculoList
                        .FirstOrDefault(x => x.FaturamentoServicoTipoVeiculoId == item.IdentificadorServicoAssociadoTipoVeiculo);

                    switch (FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.FaturamentoServicoTipo.TipoCobranca)
                    {
                        case "V":

                            FaturamentoServicoGrv.Valor = decimal.Parse(item.ValorTipoCobrancaInformado.Replace(".", ","));

                            break;

                        case "D":
                        case "P":
                        case "Q":
                        case "T":

                            FaturamentoServicoGrv.Valor = int.Parse(item.ValorTipoCobrancaInformado);

                            break;

                        case "H":

                            FaturamentoServicoGrv.TempoTrabalhado = item.ValorTipoCobrancaInformado;

                            break;
                    }

                    Grv.ListagemFaturamentoServicoGrv.Add(FaturamentoServicoGrv);
                }
            }

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Grv.Update(Grv);

                    foreach (var item in ListagemCadastroCondutorEquipamentoOpcional)
                    {
                        if (item.CondutorEquipamentoOpcionalId > 0)
                        {
                            _context.CondutorEquipamentoOpcional.Update(item);
                        }
                        else
                        {
                            _context.CondutorEquipamentoOpcional.Add(item);
                        }
                    }

                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return MensagemViewHelper.SetInternalServerError(ex);
                }
            }

            if (GgvPersistencia.ListagemFotos?.Count > 0)
            {
                List<TabelaGenericaModel> ListagemTipoCadastroFoto = await TabelaGenericaService
                        .ListAsync("GGV_TIPO_CADASTRO_FOTO");

                List<BucketFileModel> Files = new();

                foreach (CadastroFotoTipoCadastroViewModel item in GgvPersistencia.ListagemFotos)
                {
                    string TipoCadastro = ListagemTipoCadastroFoto
                        .Where(x => x.TabelaGenericaId == item.IdentificadorTipoCadastro)
                        .Select(x => x.ValorCadastro)
                        .FirstOrDefault();

                    Files.Add(new BucketFileModel
                    {
                        TipoCadastro = TipoCadastro,
                        File = item.Foto
                    });
                }

                new BucketService(_context, _httpClientFactory)
                    .SendFiles("GGVFOTOSVEICCAD",
                        GgvPersistencia.IdentificadorGrv,
                        GgvPersistencia.IdentificadorUsuario,
                        Files);
            }

            return MensagemViewHelper.SetCreateSuccess(1);
        }

        public async Task<MensagemViewModel> CreateFotosAsync(CadastroFotoGgvViewModel Fotos)
        {
            MensagemViewModel ResultView = new GrvService(_context)
                .ValidateInputGrv(Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Fotos.ListagemFotos?.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = new GrvService(_context).GetById(Fotos.IdentificadorGrv);

            if (!new[] { "V", "L", "U", "T", "R", "E", "B", "D", "1", "2", "3", "4" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<BucketFileModel> Files = new();

            List<TabelaGenericaModel> ListagemTipoCadastroFoto = await new TabelaGenericaService(_context)
                .ListAsync("GGV_TIPO_CADASTRO_FOTO");

            foreach (CadastroFotoTipoCadastroViewModel item in Fotos.ListagemFotos)
            {
                string TipoCadastro = ListagemTipoCadastroFoto
                    .Where(x => x.TabelaGenericaId == item.IdentificadorTipoCadastro)
                    .Select(x => x.ValorCadastro)
                    .FirstOrDefault();

                Files.Add(new BucketFileModel { TipoCadastro = TipoCadastro, File = item.Foto });
            }

            new BucketService(_context, _httpClientFactory)
                .SendFiles("GGVFOTOSVEICCAD", Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario, Files);

            return MensagemViewHelper.SetCreateSuccess(Fotos.ListagemFotos.Count);
        }

        public async Task<MensagemViewModel> DeleteFotosAsync(int GrvId, int UsuarioId, List<int> ListagemTabelaOrigemId)
        {
            if (ListagemTabelaOrigemId.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Informe os Identificadores das Fotos");
            }

            MensagemViewModel ResultView = new GrvService(_context).ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await new GrvService(_context).GetByIdAsync(GrvId);

            if (!new[] { "E", "B", "D", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite a exclusão de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
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

                return MensagemViewHelper.SetBadRequest(erros);
            }

            new BucketService(_context, _httpClientFactory)
                .DeleteFiles("GGVFOTOSVEICCAD", ListagemTabelaOrigemId);

            return MensagemViewHelper.SetDeleteSuccess(BucketArquivos.Count, "Foto(s) excluída(s) com sucesso");
        }

        public async Task<DadosMestresViewModel> ListDadosMestresAsync(int GrvId, int UsuarioId)
        {
            TabelaGenericaService TabelaGenericaService = new(_context, _mapper);

            VistoriaService VistoriaService = new(_context, _mapper);

            DadosMestresViewModel DadosMestres = new()
            {
                Mensagem = MensagemViewHelper.SetOk(),

                ListagemEmpresa = await new EmpresaService(_context, _mapper)
                    .ListAsync(),

                ListagemCorOstentada = await new SistemaService(_context, _mapper)
                    .ListarCorAsync(),

                ListagemEstadoGeralVeiculo = await TabelaGenericaService
                    .ListToViewModelAsync("VISTORIA_ESTADO_GERAL_VEICULO"),

                ListagemSituacaoChassi = await VistoriaService
                    .ListSituacaoChassiAsync(),

                ListagemStatusVistoria = await VistoriaService
                    .ListStatusVistoriaAsync(),

                ListagemTipoAvaria = await new TipoAvariaService(_context, _mapper)
                    .ListTipoAvariaAsync(),

                ListagemTipoCadastroFotoGGV = await TabelaGenericaService
                    .ListToViewModelAsync("GGV_TIPO_CADASTRO_FOTO"),

                ListagemServicoAssociadoVeiculo = await new FaturamentoService(_context)
                    .ListServicoAssociadoTipoVeiculoAsync(GrvId, UsuarioId)
            };

            return DadosMestres;
        }

        public async Task<ImageViewModelList> ListFotosAsync(int GrvId, int UsuarioId)
        {
            ImageViewModelList ResultView = new()
            {
                Mensagem = new GrvService(_context).ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await new BucketService(_context, _httpClientFactory)
                .DownloadFileAsync("GGVFOTOSVEICCAD", GrvId);
        }

        public async Task<MensagemViewModel> ValidarInformacoesPersistenciaAsync(CadastroGgvViewModel GgvPersistencia)
        {
            if (GgvPersistencia == null)
            {
                return MensagemViewHelper.SetBadRequest("O Modelo está nulo");
            }

            MensagemViewModel ResultView = new GrvService(_context)
                .ValidateInputGrv(GgvPersistencia.IdentificadorGrv, GgvPersistencia.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            List<string> erros = new();

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .Include(x => x.Deposito)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GgvPersistencia.IdentificadorGrv);

            if (Grv.StatusOperacao.StatusOperacaoId != "G" && Grv.StatusOperacao.StatusOperacaoId != "V")
            {
                erros.Add($"O Status do GRV não está apto para o cadastro do GGV. " +
                    $"Status atual: {Grv.StatusOperacao.Descricao.ToUpper()}");
            }

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);

            if (GgvPersistencia.DataHoraGuarda.Date > DataHoraPorDeposito.Date)
            {
                erros.Add("A Data da Guarda não pode ser maior do que a Data atual");
            }

            if (GgvPersistencia.DataHoraGuarda.Hour == 0 && GgvPersistencia.DataHoraGuarda.Minute == 0)
            {
                erros.Add("A Hora da Guarda não pode ser igual a 00:00");
            }

            if (Grv.DataHoraRemocao > GgvPersistencia.DataHoraGuarda)
            {
                erros.Add("A Data/Hora da Guarda não pode ser maior à Data/Hora da Remoção");
            }

            if (Grv.DataHoraRemocao == GgvPersistencia.DataHoraGuarda)
            {
                erros.Add("A Data/Hora da Guarda não pode ser igual à Data/Hora da Remoção");
            }

            if (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda == 0)
            {
                Grv.Deposito.GrvLimiteMinimoDatahoraGuarda = 20; // Anos
            }

            if (((DataHoraPorDeposito.Date - GgvPersistencia.DataHoraGuarda.Date).TotalDays) > (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda * 365))
            {
                if (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda == 1)
                {
                    erros.Add("A Data da Guarda não pode ser inferior a 1 ano");
                }
                else
                {
                    erros.Add("A Data da Guarda não pode ser inferior a " + Grv.Deposito.GrvLimiteMinimoDatahoraGuarda + " anos");
                }
            }

            if (GgvPersistencia.FlagChaveDeposito == "S" && string.IsNullOrWhiteSpace(GgvPersistencia.NumeroChave))
            {
                erros.Add("Informe o Número da Chave do Veículo");
            }

            if (GgvPersistencia.FlagTransbordo == "S")
            {
                if (!GgvPersistencia.DataTransbordo.HasValue)
                {
                    erros.Add("Data do Transbordo inválida");
                }
                else if (GgvPersistencia.DataTransbordo.Value > DataHoraPorDeposito)
                {
                    erros.Add("Data do Transbordo não pode ser maior do que a Data/Hora atual");
                }
            }

            if (Grv.Deposito.GrvMinimoFotosExigidas > 0)
            {
                if (GgvPersistencia.ListagemFotos?.Count == 0)
                {
                    erros.Add("É necessário enviar pelo menos 1 Foto do Veículo");
                }
            }
            else if (GgvPersistencia.ListagemFotos?.Count > 0)
            {
                if (Grv.Deposito.GrvMinimoFotosExigidas > GgvPersistencia.ListagemFotos.Count)
                {
                    erros.Add($"É necessário enviar pelo menos {Grv.Deposito.GrvMinimoFotosExigidas} Fotos do Veículo");
                }

                int count = GgvPersistencia.ListagemFotos
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

                TabelaGenericaService TabelaGenericaService = new(_context, _mapper);

                List<int> ListagemTipoCadastroId = GgvPersistencia.ListagemFotos
                    .Where(x => x.IdentificadorTipoCadastro > 0)
                    .Select(x => x.IdentificadorTipoCadastro)
                    .ToList();

                if (ListagemTipoCadastroId.Count > 0)
                {
                    List<TabelaGenericaModel> ListagemTipoCadastroFoto = await TabelaGenericaService
                        .ListAsync("GGV_TIPO_CADASTRO_FOTO");

                    List<int> ListagemTipoCadastroId2 = ListagemTipoCadastroFoto
                        .Select(x => x.TabelaGenericaId)
                        .ToList();

                    int result = ListagemTipoCadastroId
                        .Where(x => ListagemTipoCadastroId2.All(x2 => x2 != x))
                        .Count();

                    if (result >= 1)
                    {
                        erros.Add($"Foram indentificados {count} Identificador do Tipo do Cadastro da Foto inexistente");
                    }
                }
            }

            if (GgvPersistencia.ListagemEquipamentoOpcional?.Count > 0)
            {
                if (GgvPersistencia.ListagemEquipamentoOpcional.Where(x => x.IdentificadorEquipamentoOpcional <= 0).ToList().Count > 0)
                {
                    erros.Add("Existe um ou mais Identificador do Equipamento Opcional inválido");
                }

                if (GgvPersistencia.ListagemEquipamentoOpcional.Where(x => x.FlagEquipamentoAvariado == "S" && (x.IdentificadorTipoAvaria <= 0 || x.IdentificadorTipoAvaria == null)).ToList().Count > 0)
                {
                    erros.Add("Existe um ou mais Identificador do Tipo de Avaria inválido");
                }
            }

            if (GgvPersistencia.Vistoria != null)
            {
                if (GgvPersistencia.Vistoria.FlagVistoria == "S")
                {
                    if (GgvPersistencia.Vistoria.IdentificadorEmpresaVistoria > 0)
                    {
                        if (await _context.Empresa
                            .AsNoTracking()
                            .FirstOrDefaultAsync(w => w.EmpresaId == GgvPersistencia.Vistoria.IdentificadorEmpresaVistoria) == null)
                        {
                            erros.Add("Identificador da Empresa inexistente");
                        }
                    }

                    if (GgvPersistencia.Vistoria.IdentificadorStatusVistoria > 0)
                    {
                        if (await _context.VistoriaStatus
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.VistoriaStatusId == GgvPersistencia.Vistoria.IdentificadorStatusVistoria) == null)
                        {
                            erros.Add("Identificador do Status da Vistoria inexistente");
                        }
                    }

                    if (await _context.VistoriaSituacaoChassi
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.VistoriaSituacaoChassiId == GgvPersistencia.Vistoria.IdentificadorSituacaoChassi) == null)
                    {
                        erros.Add("Identificador da Situação do Chassi inexistente");
                    }

                    if (GgvPersistencia.Vistoria.IdentificadorTipoDirecao > 0)
                    {
                        if (await _context.TabelaGenerica
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Codigo == "VISTORIA_TIPO_DIRECAO"
                                                   && x.TabelaGenericaId == GgvPersistencia.Vistoria.IdentificadorTipoDirecao) == null)
                        {
                            erros.Add("Identificador do Tipo de Direção inexistente");
                        }
                    }

                    if (await _context.TabelaGenerica
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Codigo == "VISTORIA_ESTADO_GERAL_VEICULO"
                                                   && x.TabelaGenericaId == GgvPersistencia.Vistoria.IdentificadorEstadoGeralVeiculo) == null)
                    {
                        erros.Add("Identificador do Estado Geral do Veículo inexistente");
                    }

                    if (GgvPersistencia.Vistoria.DataVistoria > DataHoraPorDeposito)
                    {
                        erros.Add("A Data da Vistoria não pode ser maior do que a Data Atual");
                    }

                    if (GgvPersistencia.Vistoria.FlagPossuiPlaca == "S")
                    {
                        if (string.IsNullOrWhiteSpace(GgvPersistencia.Vistoria.PlacaOstentada))
                        {
                            erros.Add("Informe a Placa Ostentada");
                        }
                        else if (!GgvPersistencia.Vistoria.PlacaOstentada.IsPlaca())
                        {
                            erros.Add("Placa Ostentada inválida");
                        }
                    }
                }
            }

            if (GgvPersistencia.ListagemFaturamentoServicoGrv?.Count > 0)
            {
                int duplicados = GgvPersistencia.ListagemFaturamentoServicoGrv
                    .Where(x => x.IdentificadorServicoAssociadoTipoVeiculo > 0)
                    .Select(x => x.IdentificadorServicoAssociadoTipoVeiculo)
                    .GroupBy(x => x)
                    .Where(x => x.Count() > 1)
                    .Count();

                if (duplicados > 0)
                {
                    erros.Add("Existem Serviços duplicados");
                }

                int invalidos = GgvPersistencia.ListagemFaturamentoServicoGrv
                    .Where(x => x.IdentificadorServicoAssociadoTipoVeiculo <= 0)
                    .Count();

                if (invalidos > 0)
                {
                    erros.Add("Existem Identificador dos Serviços inválidos");
                }

                if (duplicados == 0 && invalidos == 0)
                {
                    List<int> ids = GgvPersistencia.ListagemFaturamentoServicoGrv
                        .Where(x => x.IdentificadorServicoAssociadoTipoVeiculo > 0)
                        .Select(x => x.IdentificadorServicoAssociadoTipoVeiculo)
                        .Distinct()
                        .ToList();

                    List<FaturamentoServicoTipoVeiculoModel> FaturamentoServicoTipoVeiculoList = await _context.FaturamentoServicoTipoVeiculo
                        .Include(x => x.FaturamentoServicoAssociado)
                        .ThenInclude(x => x.FaturamentoServicoTipo)
                        .Where(x => ids.Contains(x.FaturamentoServicoTipoVeiculoId))
                        .AsNoTracking()
                        .ToListAsync();

                    if (ids.Count != FaturamentoServicoTipoVeiculoList.Count)
                    {
                        erros.Add("A listagem de Serviço possui um ou mais Identificador inexistente");
                    }
                    else
                    {
                        FaturamentoServicoTipoVeiculoModel FaturamentoServicoTipoVeiculo = new();

                        FaturamentoServicoGrvModel FaturamentoServicoGrv = new();

                        List<FaturamentoServicoGrvModel> FaturamentoServicoGrvList = _context.FaturamentoServicoGrv
                            .Include(x => x.FaturamentoServicoTipoVeiculo)
                            .ThenInclude(x => x.FaturamentoServicoAssociado)
                            .Where(x => x.GrvId == GgvPersistencia.IdentificadorGrv)
                            .AsNoTracking()
                            .ToList();

                        foreach (var item in GgvPersistencia.ListagemFaturamentoServicoGrv)
                        {
                            if (FaturamentoServicoGrvList?.Count > 0)
                            {
                                FaturamentoServicoGrv = FaturamentoServicoGrvList
                                    .FirstOrDefault(x => x.FaturamentoServicoTipoVeiculoId == item.IdentificadorServicoAssociadoTipoVeiculo);

                                if (FaturamentoServicoGrv != null)
                                {
                                    erros.Add($"O Serviço {FaturamentoServicoGrv.FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.Descricao} já está cadastrado para este GRV");
                                }
                            }
                            else
                            {
                                FaturamentoServicoTipoVeiculo = FaturamentoServicoTipoVeiculoList
                                    .FirstOrDefault(x => x.FaturamentoServicoTipoVeiculoId == item.IdentificadorServicoAssociadoTipoVeiculo);

                                if (FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.FaturamentoServicoTipo.FlagCobrarTelaGrv == "N")
                                {
                                    erros.Add($"Foi identificado um Serviço que não pode ser cobrado antes do Atendimento. Serviço informado: {FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.Descricao}");
                                }
                                else
                                {
                                    item.ValorTipoCobrancaInformado = item.ValorTipoCobrancaInformado.Replace(".", ",");

                                    switch (FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.FaturamentoServicoTipo.TipoCobranca)
                                    {
                                        // Diárias
                                        case "D":

                                            if (!NumberHelper.IsNumber(item.ValorTipoCobrancaInformado))
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Diárias inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                            }

                                            break;

                                        // Horas
                                        case "H":

                                            if (!NumberHelper.IsNumber(item.ValorTipoCobrancaInformado.RemoveString(":")))
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Horas inválido. Valor informado: {item.ValorTipoCobrancaInformado}. Informe no padrão HH:MM");
                                            }
                                            else
                                            {
                                                string[] aux = item.ValorTipoCobrancaInformado.Split(':');

                                                if (aux.Length != 2)
                                                {
                                                    erros.Add($"Valor do Tipo de Cobrança Horas inválido. Valor informado: {item.ValorTipoCobrancaInformado}. Informe no padrão HH:MM");
                                                }
                                                else if (int.Parse(aux[0]) < 0 || int.Parse(aux[0]) > 23 || int.Parse(aux[1]) < 0 || int.Parse(aux[1]) > 59)
                                                {
                                                    erros.Add($"Valor do Tipo de Cobrança Horas inválido. Valor informado: {item.ValorTipoCobrancaInformado}. Informe no padrão HH:MM");
                                                }
                                                else if (int.Parse(aux[0]) == 0 && int.Parse(aux[1]) == 0)
                                                {
                                                    erros.Add($"Informe o Tempo trabalhado");
                                                }
                                            }

                                            break;

                                        // Percentual
                                        case "P":

                                            if (!NumberHelper.IsNumber(item.ValorTipoCobrancaInformado))
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Percentual inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                            }
                                            else if (int.Parse(item.ValorTipoCobrancaInformado) < 0 || int.Parse(item.ValorTipoCobrancaInformado) > 100)
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Percentual inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                            }

                                            break;

                                        // Quantidade
                                        case "Q":

                                            if (!NumberHelper.IsNumber(item.ValorTipoCobrancaInformado))
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Quantidade inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                            }
                                            else if (int.Parse(item.ValorTipoCobrancaInformado) < 0)
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Quantidade inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                            }

                                            break;

                                        // Tempo/Espaço
                                        case "T":

                                            if (!NumberHelper.IsNumber(item.ValorTipoCobrancaInformado))
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Tempo inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                            }
                                            else if (int.Parse(item.ValorTipoCobrancaInformado) < 0)
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Tempo inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                            }

                                            break;

                                        // Valor Monetário
                                        case "V":

                                            if (!NumberHelper.IsNumber(item.ValorTipoCobrancaInformado.RemoveStrings(new[] { ".", "," })))
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Valor Monetário inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                            }
                                            else if (!decimal.TryParse(item.ValorTipoCobrancaInformado, out _))
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Valor Monetário inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                            }
                                            else if (decimal.Parse(item.ValorTipoCobrancaInformado) < 0)
                                            {
                                                erros.Add($"Valor do Tipo de Cobrança Valor Monetário inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                            }
                                            else
                                            {
                                                if (FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.FlagPermiteAlteracaoValor == "N"
                                                    && FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.PrecoPadrao != decimal.Parse(item.ValorTipoCobrancaInformado))
                                                {
                                                    erros.Add($"Valor do Tipo de Cobrança Valor Monetário não pode ser diferente do Preço Padrão. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }
                                                else if (decimal.Parse(item.ValorTipoCobrancaInformado) < FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.PrecoValorMinimo)
                                                {
                                                    erros.Add($"Valor do Tipo de Cobrança Valor Monetário não pode ser menor do que o valor do Preço Mínimo parametrizado" +
                                                        $". Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }
                                            }

                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return erros.Count == 0 ? MensagemViewHelper.SetOk() : MensagemViewHelper.SetBadRequest(erros);
        }
    }
}
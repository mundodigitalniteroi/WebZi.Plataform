using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Empresa;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Vistorias;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.GGV;
using WebZi.Plataform.Domain.DTO.Faturamento.Servico;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Models.Vistoria;
using WebZi.Plataform.Domain.ViewModel.GGV;
using WebZi.Plataform.Domain.Views.Faturamento;

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

        public async Task<MensagemDTO> UpdateGgvAsync(GgvParameters GgvPersistencia, CancellationToken ct)
        {
            MensagemDTO ResultView = await ValidarInformacoesPersistenciaAsync(GgvPersistencia, true);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.Vistoria)
                .Include(x => x.ListagemFaturamentoServicoGrv)
                .FirstOrDefaultAsync(x => x.GrvId == GgvPersistencia.IdentificadorProcesso, cancellationToken: ct);

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);

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

            TabelaGenericaService TabelaGenericaService = new(_context);

            if (GgvPersistencia.Vistoria != null)
            {
                VistoriaModel Vistoria = Grv.Vistoria ?? new VistoriaModel
                {
                    UsuarioCadastroId = GgvPersistencia.IdentificadorUsuario,
                    DataCadastro = DataHoraPorDeposito
                };

                Vistoria.UsuarioAlteracaoId = GgvPersistencia.IdentificadorUsuario;
                Vistoria.DataAlteracao = DataHoraPorDeposito;

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

                    Vistoria.MotivoNaoRealizacaoVistoria = null;

                    Vistoria.FlagPossuiRestricoes = GgvPersistencia.Vistoria.FlagPossuiRestricoes;

                    Vistoria.FlagPossuiVidroEletrico = GgvPersistencia.Vistoria.FlagPossuiVidroEletrico;

                    Vistoria.FlagPossuiTravaEletrica = GgvPersistencia.Vistoria.FlagPossuiTravaEletrica;

                    Vistoria.FlagPossuiPlaca = GgvPersistencia.Vistoria.FlagPossuiPlaca;

                    Vistoria.EmpresaVistoriaId = GgvPersistencia.Vistoria.IdentificadorEmpresaVistoria;

                    Vistoria.NumeroVistoria = GgvPersistencia.Vistoria.NumeroVistoria.ToUpperTrim().ToNullIfEmpty();

                    Vistoria.NomeVistoriador = GgvPersistencia.Vistoria.NomeVistoriador.ToUpperTrim().ToNullIfEmpty();

                    Vistoria.NumeroMotor = GgvPersistencia.Vistoria.NumeroMotor.ToUpperTrim().ToNullIfEmpty();

                    Vistoria.DataVistoria = GgvPersistencia.Vistoria.DataVistoria;

                    Vistoria.ResumoVistoria = GgvPersistencia.Vistoria.ResumoVistoria.ToUpperTrim().ToNullIfEmpty();

                    Vistoria.VistoriaStatusId = (await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context
                                .VistoriaStatus
                                .AsNoTracking(), x =>
                                x.VistoriaStatusId == (byte)GgvPersistencia.Vistoria.IdentificadorStatusVistoria,
                            cancellationToken: ct))?
                        .VistoriaStatusId;

                    Vistoria.VistoriaSituacaoChassiId = (await _context.VistoriaSituacaoChassi
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                    x.VistoriaSituacaoChassiId == GgvPersistencia.Vistoria.IdentificadorSituacaoChassi,
                                cancellationToken: ct))?
                        .VistoriaSituacaoChassiId;

                    // VISTORIA_TIPO_DIRECAO
                    Vistoria.TipoDirecao =
                        await TabelaGenericaService.GetValorCadastroAsync(GgvPersistencia.Vistoria
                            .IdentificadorTipoDirecao);

                    // VISTORIA_ESTADO_GERAL_VEICULO
                    Vistoria.EstadoGeralVeiculo =
                        await TabelaGenericaService.GetValorCadastroAsync(GgvPersistencia.Vistoria
                            .IdentificadorEstadoGeralVeiculo);

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
                Grv.Vistoria = null;
            }

            if (GgvPersistencia.ListagemFaturamentoServicoGrv?.Count > 0)
            {
                if (Grv.ListagemFaturamentoServicoGrv == null)
                {
                    Grv.ListagemFaturamentoServicoGrv = new HashSet<FaturamentoServicoGrvModel>();
                }

                List<int> ids = GgvPersistencia.ListagemFaturamentoServicoGrv
                    .Where(x => x.IdentificadorServicoAssociadoTipoVeiculo > 0)
                    .Select(x => x.IdentificadorServicoAssociadoTipoVeiculo)
                    .Distinct()
                    .ToList();

                List<FaturamentoServicoTipoVeiculoModel> FaturamentoServicoTipoVeiculoList = await _context
                    .FaturamentoServicoTipoVeiculo
                    .Include(x => x.FaturamentoServicoAssociado)
                    .ThenInclude(x => x.FaturamentoServicoTipo)
                    .Where(x => ids.Contains(x.FaturamentoServicoTipoVeiculoId))
                    .AsNoTracking()
                    .ToListAsync(cancellationToken: ct);

                FaturamentoServicoTipoVeiculoModel FaturamentoServicoTipoVeiculo = new();

                FaturamentoServicoGrvModel FaturamentoServicoGrv = new();

                foreach (FaturamentoServicoGrvParameters item in GgvPersistencia.ListagemFaturamentoServicoGrv)
                {
                    FaturamentoServicoTipoVeiculo = FaturamentoServicoTipoVeiculoList
                        .FirstOrDefault(x =>
                            x.FaturamentoServicoTipoVeiculoId == item.IdentificadorServicoAssociadoTipoVeiculo);

                    if (FaturamentoServicoTipoVeiculo == null)
                    {
                        continue;
                    }

                    FaturamentoServicoGrv = Grv.ListagemFaturamentoServicoGrv
                        .FirstOrDefault(x =>
                            x.FaturamentoServicoTipoVeiculoId == item.IdentificadorServicoAssociadoTipoVeiculo);

                    if (FaturamentoServicoGrv == null)
                    {
                        FaturamentoServicoGrv = new()
                        {
                            GrvId = GgvPersistencia.IdentificadorProcesso,
                            FaturamentoServicoTipoVeiculoId = item.IdentificadorServicoAssociadoTipoVeiculo
                        };

                        Grv.ListagemFaturamentoServicoGrv.Add(FaturamentoServicoGrv);
                    }

                    FaturamentoServicoGrv.QuantidadeDesconto = item.Quantidade;
                    FaturamentoServicoGrv.FlagRealizarCobranca = item.FlagCobranca;

                    if (string.IsNullOrWhiteSpace(item.ValorTipoCobrancaInformado) &&
                        !string.IsNullOrWhiteSpace(item.HoraMinuto))
                    {
                        item.ValorTipoCobrancaInformado = item.HoraMinuto;
                    }

                    switch (FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.FaturamentoServicoTipo
                                .TipoCobranca)
                    {
                        case "V":
                        case "D":
                        case "P":
                        case "Q":
                        case "T":

                            FaturamentoServicoGrv.Valor =
                                decimal.Parse(item.ValorTipoCobrancaInformado.Replace(".", ","));

                            break;

                        case "H":

                            FaturamentoServicoGrv.TempoTrabalhado = !string.IsNullOrWhiteSpace(item.HoraMinuto)
                                ? item.HoraMinuto
                                : item.ValorTipoCobrancaInformado;

                            FaturamentoServicoGrv.Valor = !string.IsNullOrWhiteSpace(item.ValorTipoCobrancaInformado) &&
                                                          decimal.TryParse(item.ValorTipoCobrancaInformado.Replace(".", ","),
                                                              out decimal valorInformadoUpdateH)
                                ? valorInformadoUpdateH
                                : FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.PrecoPadrao;

                            break;
                    }
                }
            }

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(ct))
            {
                try
                {
                    _context.Grv.Update(Grv);

                    // foreach (CondutorEquipamentoOpcionalModel item in ListagemCadastroCondutorEquipamentoOpcional)
                    // {
                    //     if (item.CondutorEquipamentoOpcionalId > 0)
                    //     {
                    //         _context.CondutorEquipamentoOpcional.Update(item);
                    //     }
                    //     else
                    //     {
                    //         _context.CondutorEquipamentoOpcional.Add(item);
                    //     }
                    // }

                    await _context.SaveChangesAsync(ct);

                    await transaction.CommitAsync(ct);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(ct);

                    ResultView = MensagemViewHelper.SetInternalServerError(ex);

                    return ResultView;
                }
            }

            if (GgvPersistencia.ListagemFotos?.Count > 0)
            {
                List<TabelaGenericaModel> ListagemTipoCadastroFoto = await TabelaGenericaService
                    .ListAsync("GGV_TIPO_CADASTRO_FOTO");

                List<BucketFileModel> Files = new();

                foreach (FotoTipoCadastroParameters item in GgvPersistencia.ListagemFotos)
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
                    .SendFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGGV,
                        GgvPersistencia.IdentificadorProcesso,
                        GgvPersistencia.IdentificadorUsuario,
                        Files);
            }

            return MensagemViewHelper.SetUpdateSuccess();
        }


        public async Task<MensagemDTO> CreateGgvAsync(GgvParameters GgvPersistencia, CancellationToken ct)
        {
            MensagemDTO ResultView = await ValidarInformacoesPersistenciaAsync(GgvPersistencia, false);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.ListagemFaturamentoServicoGrv)
                .FirstOrDefaultAsync(x => x.GrvId == GgvPersistencia.IdentificadorProcesso, cancellationToken: ct);

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

            // List<CondutorEquipamentoOpcionalModel> ListagemCadastroCondutorEquipamentoOpcional = new();

            // if (GgvPersistencia.ListagemEquipamentoOpcional?.Count > 0)
            // {
            //     List<decimal> EquipamentoOpcionalIds = GgvPersistencia.ListagemEquipamentoOpcional
            //         .Select(x => x.IdentificadorEquipamentoOpcional)
            //         .Distinct()
            //         .ToList();
            //
            //     List<CondutorEquipamentoOpcionalModel> ListagemCondutorEquipamentoOpcional = await _context.CondutorEquipamentoOpcional
            //         .Where(x => EquipamentoOpcionalIds.Contains(x.EquipamentoOpcionalId) && x.GrvId == Grv.GrvId)
            //         .AsNoTracking()
            //         .ToListAsync();
            //
            //     Grv.ListagemCondutorEquipamentoOpcional = new HashSet<CondutorEquipamentoOpcionalModel>();
            //
            //     CondutorEquipamentoOpcionalModel CadastroCondutorEquipamentoOpcional = new();
            //
            //     CondutorEquipamentoOpcionalModel CondutorEquipamentoOpcional = new();
            //
            //     List<TipoAvariaModel> ListagemTipoAvaria = await _context.TipoAvaria
            //         .AsNoTracking()
            //         .ToListAsync();
            //
            //     foreach (EquipamentoOpcionalParameters item in GgvPersistencia.ListagemEquipamentoOpcional)
            //     {
            //         CadastroCondutorEquipamentoOpcional = new()
            //         {
            //             GrvId = Grv.GrvId,
            //
            //             EquipamentoOpcionalId = item.IdentificadorEquipamentoOpcional,
            //
            //             FlagPossuiEquipamento = item.FlagPossuiEquipamento
            //         };
            //
            //         CondutorEquipamentoOpcional = ListagemCondutorEquipamentoOpcional
            //             .FirstOrDefault(x => x.EquipamentoOpcionalId == item.IdentificadorEquipamentoOpcional);
            //
            //         // Já possui cadastro
            //         if (CondutorEquipamentoOpcional != null)
            //         {
            //             if (CondutorEquipamentoOpcional.FlagPossuiEquipamento == item.FlagPossuiEquipamento
            //              && (CondutorEquipamentoOpcional.FlagEquipamentoAvariado == item.FlagEquipamentoAvariado && CondutorEquipamentoOpcional.CodigoAvaria == item.IdentificadorTipoAvaria))
            //             {
            //                 continue;
            //             }
            //
            //             CadastroCondutorEquipamentoOpcional.CondutorEquipamentoOpcionalId = CondutorEquipamentoOpcional.CondutorEquipamentoOpcionalId;
            //
            //             CadastroCondutorEquipamentoOpcional.UsuarioAlteracaoId = GgvPersistencia.IdentificadorUsuario;
            //
            //             CadastroCondutorEquipamentoOpcional.DataAtualizacao = DataHoraPorDeposito;
            //         }
            //         else
            //         {
            //             CadastroCondutorEquipamentoOpcional = new()
            //             {
            //                 GrvId = Grv.GrvId,
            //
            //                 EquipamentoOpcionalId = item.IdentificadorEquipamentoOpcional,
            //
            //                 FlagPossuiEquipamento = item.FlagPossuiEquipamento,
            //
            //                 UsuarioCadastroId = GgvPersistencia.IdentificadorUsuario
            //             };
            //         }
            //
            //         if (item.FlagPossuiEquipamento == "S")
            //         {
            //             CadastroCondutorEquipamentoOpcional.FlagEquipamentoAvariado = item.FlagEquipamentoAvariado;
            //
            //             if (item.FlagEquipamentoAvariado == "S")
            //             {
            //                 CadastroCondutorEquipamentoOpcional.CodigoAvaria = item.IdentificadorTipoAvaria;
            //             }
            //         }
            //         else
            //         {
            //             CadastroCondutorEquipamentoOpcional.FlagEquipamentoAvariado = null;
            //
            //             CadastroCondutorEquipamentoOpcional.CodigoAvaria = null;
            //         }
            //
            //         ListagemCadastroCondutorEquipamentoOpcional.Add(CadastroCondutorEquipamentoOpcional);
            //     }
            // }

            TabelaGenericaService TabelaGenericaService = new(_context);

            if (GgvPersistencia.Vistoria != null)
            {
                VistoriaModel Vistoria = new()
                {
                    UsuarioCadastroId = GgvPersistencia.IdentificadorUsuario,
                    DataCadastro = DataHoraPorDeposito
                };

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

                    Vistoria.MotivoNaoRealizacaoVistoria = null;

                    Vistoria.FlagPossuiRestricoes = GgvPersistencia.Vistoria.FlagPossuiRestricoes;

                    Vistoria.FlagPossuiVidroEletrico = GgvPersistencia.Vistoria.FlagPossuiVidroEletrico;

                    Vistoria.FlagPossuiTravaEletrica = GgvPersistencia.Vistoria.FlagPossuiTravaEletrica;

                    Vistoria.FlagPossuiPlaca = GgvPersistencia.Vistoria.FlagPossuiPlaca;

                    Vistoria.EmpresaVistoriaId = GgvPersistencia.Vistoria.IdentificadorEmpresaVistoria;

                    Vistoria.NumeroVistoria = GgvPersistencia.Vistoria.NumeroVistoria.ToUpperTrim().ToNullIfEmpty();

                    Vistoria.NomeVistoriador = GgvPersistencia.Vistoria.NomeVistoriador.ToUpperTrim().ToNullIfEmpty();

                    Vistoria.NumeroMotor = GgvPersistencia.Vistoria.NumeroMotor.ToUpperTrim().ToNullIfEmpty();

                    Vistoria.DataVistoria = GgvPersistencia.Vistoria.DataVistoria;

                    Vistoria.ResumoVistoria = GgvPersistencia.Vistoria.ResumoVistoria.ToUpperTrim().ToNullIfEmpty();

                    Vistoria.VistoriaStatusId = (await _context.VistoriaStatus
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                    x.VistoriaStatusId == (byte)GgvPersistencia.Vistoria.IdentificadorStatusVistoria,
                                cancellationToken: ct))?
                        .VistoriaStatusId;

                    Vistoria.VistoriaSituacaoChassiId = (await _context.VistoriaSituacaoChassi
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                    x.VistoriaSituacaoChassiId == GgvPersistencia.Vistoria.IdentificadorSituacaoChassi,
                                cancellationToken: ct))?
                        .VistoriaSituacaoChassiId;

                    // VISTORIA_TIPO_DIRECAO
                    Vistoria.TipoDirecao =
                        await TabelaGenericaService.GetValorCadastroAsync(GgvPersistencia.Vistoria
                            .IdentificadorTipoDirecao);

                    // VISTORIA_ESTADO_GERAL_VEICULO
                    Vistoria.EstadoGeralVeiculo =
                        await TabelaGenericaService.GetValorCadastroAsync(GgvPersistencia.Vistoria
                            .IdentificadorEstadoGeralVeiculo);

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
                Grv.Vistoria = null;
            }

            if (GgvPersistencia.ListagemFaturamentoServicoGrv?.Count > 0)
            {
                if (Grv.ListagemFaturamentoServicoGrv == null)
                {
                    Grv.ListagemFaturamentoServicoGrv = new HashSet<FaturamentoServicoGrvModel>();
                }

                List<int> ids = GgvPersistencia.ListagemFaturamentoServicoGrv
                    .Where(x => x.IdentificadorServicoAssociadoTipoVeiculo > 0)
                    .Select(x => x.IdentificadorServicoAssociadoTipoVeiculo)
                    .Distinct()
                    .ToList();

                List<FaturamentoServicoTipoVeiculoModel> FaturamentoServicoTipoVeiculoList = await _context
                    .FaturamentoServicoTipoVeiculo
                    .Include(x => x.FaturamentoServicoAssociado)
                    .ThenInclude(x => x.FaturamentoServicoTipo)
                    .Where(x => ids.Contains(x.FaturamentoServicoTipoVeiculoId))
                    .AsNoTracking()
                    .ToListAsync(cancellationToken: ct);

                FaturamentoServicoTipoVeiculoModel FaturamentoServicoTipoVeiculo = new();

                FaturamentoServicoGrvModel FaturamentoServicoGrv = new();

                foreach (FaturamentoServicoGrvParameters item in GgvPersistencia.ListagemFaturamentoServicoGrv)
                {
                    FaturamentoServicoTipoVeiculo = FaturamentoServicoTipoVeiculoList
                        .FirstOrDefault(x =>
                            x.FaturamentoServicoTipoVeiculoId == item.IdentificadorServicoAssociadoTipoVeiculo);

                    if (FaturamentoServicoTipoVeiculo == null)
                    {
                        continue;
                    }

                    FaturamentoServicoGrv = Grv.ListagemFaturamentoServicoGrv
                        .FirstOrDefault(x =>
                            x.FaturamentoServicoTipoVeiculoId == item.IdentificadorServicoAssociadoTipoVeiculo);

                    if (FaturamentoServicoGrv == null)
                    {
                        FaturamentoServicoGrv = new()
                        {
                            GrvId = GgvPersistencia.IdentificadorProcesso,
                            FaturamentoServicoTipoVeiculoId = item.IdentificadorServicoAssociadoTipoVeiculo
                        };

                        Grv.ListagemFaturamentoServicoGrv.Add(FaturamentoServicoGrv);
                    }

                    FaturamentoServicoGrv.QuantidadeDesconto = item.Quantidade;
                    FaturamentoServicoGrv.FlagRealizarCobranca = item.FlagCobranca;

                    if (string.IsNullOrWhiteSpace(item.ValorTipoCobrancaInformado) &&
                        !string.IsNullOrWhiteSpace(item.HoraMinuto))
                    {
                        item.ValorTipoCobrancaInformado = item.HoraMinuto;
                    }

                    switch (FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.FaturamentoServicoTipo
                                .TipoCobranca)
                    {
                        case "V":
                        case "D":
                        case "P":
                        case "Q":
                        case "T":

                            FaturamentoServicoGrv.Valor =
                                decimal.Parse(item.ValorTipoCobrancaInformado.Replace(".", ","));

                            break;

                        case "H":

                            FaturamentoServicoGrv.TempoTrabalhado = !string.IsNullOrWhiteSpace(item.HoraMinuto)
                                ? item.HoraMinuto
                                : item.ValorTipoCobrancaInformado;

                            FaturamentoServicoGrv.Valor = !string.IsNullOrWhiteSpace(item.ValorTipoCobrancaInformado) &&
                                                          decimal.TryParse(item.ValorTipoCobrancaInformado.Replace(".", ","),
                                                              out decimal valorInformadoCreateH)
                                ? valorInformadoCreateH
                                : FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.PrecoPadrao;

                            break;
                    }
                }
            }

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(ct))
            {
                try
                {
                    _context.Grv.Update(Grv);

                    // foreach (CondutorEquipamentoOpcionalModel item in ListagemCadastroCondutorEquipamentoOpcional)
                    // {
                    //     if (item.CondutorEquipamentoOpcionalId > 0)
                    //     {
                    //         _context.CondutorEquipamentoOpcional.Update(item);
                    //     }
                    //     else
                    //     {
                    //         _context.CondutorEquipamentoOpcional.Add(item);
                    //     }
                    // }

                    await _context.SaveChangesAsync(ct);

                    await transaction.CommitAsync(ct);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(ct);

                    ResultView = MensagemViewHelper.SetInternalServerError(ex);

                    return ResultView;
                }
            }

            if (GgvPersistencia.ListagemFotos?.Count > 0)
            {
                List<TabelaGenericaModel> ListagemTipoCadastroFoto = await TabelaGenericaService
                    .ListAsync("GGV_TIPO_CADASTRO_FOTO");

                List<BucketFileModel> Files = new();

                foreach (FotoTipoCadastroParameters item in GgvPersistencia.ListagemFotos)
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
                    .SendFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGGV,
                        GgvPersistencia.IdentificadorProcesso,
                        GgvPersistencia.IdentificadorUsuario,
                        Files);
            }

            return MensagemViewHelper.SetCreateSuccess();
        }

        public async Task<MensagemDTO> CreateFotosAsync(FotoGgvParameters Fotos)
        {
            MensagemDTO ResultView = new GrvService(_context)
                .ValidateInputGrv(Fotos.IdentificadorProcesso, Fotos.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Fotos.ListagemFotos?.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = new GrvService(_context).GetById(Fotos.IdentificadorProcesso);

            if (new[] { "C" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<BucketFileModel> Files = new();

            List<TabelaGenericaModel> ListagemTipoCadastroFoto = await new TabelaGenericaService(_context)
                .ListAsync("GGV_TIPO_CADASTRO_FOTO");

            foreach (FotoTipoCadastroParameters item in Fotos.ListagemFotos)
            {
                string TipoCadastro = ListagemTipoCadastroFoto
                    .Where(x => x.TabelaGenericaId == item.IdentificadorTipoCadastro)
                    .Select(x => x.ValorCadastro)
                    .FirstOrDefault();

                Files.Add(new BucketFileModel { TipoCadastro = TipoCadastro, File = item.Foto });
            }

            new BucketService(_context, _httpClientFactory)
                .SendFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGGV, Fotos.IdentificadorProcesso,
                    Fotos.IdentificadorUsuario, Files);

            return MensagemViewHelper.SetCreateSuccess(Fotos.ListagemFotos.Count);
        }

        public async Task<MensagemDTO> DeleteFotosAsync(int GrvId, int UsuarioId, List<int> ListagemTabelaOrigemId)
        {
            if (ListagemTabelaOrigemId.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Informe os Identificadores das Fotos");
            }

            MensagemDTO ResultView = new GrvService(_context).ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await new GrvService(_context).GetByIdAsync(GrvId);

            if (!new[] { "E", "B", "D", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite a exclusão de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<BucketArquivoModel> BucketArquivos = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId != GrvId
                            && ListagemTabelaOrigemId.Contains(x.RepositorioArquivoId)
                            && x.BucketNomeTabelaOrigem.Codigo == BucketNomeTabelaOrigemEnum.FotoVeiculoGGV)
                .AsNoTracking()
                .ToListAsync();

            if (BucketArquivos?.Count > 0)
            {
                List<string> erros = new()
                {
                    $"A(s) seguinte(s) Fotos não pertencem ao Processo {GrvId}:"
                };

                foreach (BucketArquivoModel BucketArquivo in BucketArquivos)
                {
                    erros.Add(BucketArquivo.RepositorioArquivoId.ToString());
                }

                return MensagemViewHelper.SetBadRequest(erros);
            }

            new BucketService(_context, _httpClientFactory)
                .DeleteFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGGV, ListagemTabelaOrigemId, true);

            return MensagemViewHelper.SetDeleteSuccess("Foto(s) excluída(s) com sucesso");
        }

        public async Task<MensagemDTO> AddServiceAssociationAsync(AdicionarServicoAoGgvParameters parameters,
            int usuarioId, CancellationToken ct = default)
        {
            if (parameters == null || parameters.IdentificadorServicoAssociadoTipoVeiculo <= 0)
            {
                return MensagemViewHelper.SetBadRequest("Informe os Identificadores dos Serviços");
            }

            MensagemDTO ResultView =
                new GrvService(_context).ValidateInputGrv(parameters.IdentificadorProcesso, usuarioId);

            var erros = new List<string>();
            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            var grv = await _context.Grv.AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == parameters.IdentificadorProcesso, cancellationToken: ct);

            ViewFaturamentoServicoAssociadoVeiculoModel servicos = await _context
                .ViewFaturamentoServicoAssociadoVeiculo
                .FirstOrDefaultAsync(
                    x => x.FaturamentoServicoTipoVeiculoId == parameters.IdentificadorServicoAssociadoTipoVeiculo,
                    cancellationToken: ct);

            if (grv is null)
                erros.Add("Processo GRV não encontrado");

            if (servicos is null)
                erros.Add("Serviço não encontrado");

            if (string.IsNullOrWhiteSpace(parameters.ValorTipoCobrancaInformado) &&
                !string.IsNullOrWhiteSpace(parameters.HoraMinuto))
            {
                parameters.ValorTipoCobrancaInformado = parameters.HoraMinuto;
            }

            if (servicos is not null && (servicos.TipoCobranca == "H" ||
                                         servicos.TipoCobranca == TipoCobrancaFaturamentoEnum.Horas))
            {
                if (string.IsNullOrWhiteSpace(parameters.HoraMinuto) &&
                    string.IsNullOrWhiteSpace(parameters.ValorTipoCobrancaInformado))
                {
                    erros.Add("Informe o Tempo trabalhado");
                }
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.SetBadRequest(erros);
            }

            FaturamentoServicoGrvModel servicoAssociado = await _context.FaturamentoServicoGrv
                .AsTracking()
                .FirstOrDefaultAsync(
                    x => x.GrvId == parameters.IdentificadorProcesso && x.FaturamentoServicoTipoVeiculoId ==
                        parameters.IdentificadorServicoAssociadoTipoVeiculo,
                    cancellationToken: ct);

            if (servicoAssociado is not null)
            {
                return MensagemViewHelper.SetFound(
                    "Serviço ja vinculado a esse Processo");
            }

            var payload = new FaturamentoServicoGrvModel
            {
                GrvId = parameters.IdentificadorProcesso,
                FaturamentoServicoTipoVeiculoId = servicos.FaturamentoServicoTipoVeiculoId,
                QuantidadeDesconto = parameters.Quantidade,
                FlagRealizarCobranca =
                    !string.IsNullOrWhiteSpace(parameters.FlagCobranca) ? parameters.FlagCobranca : "S",
                OrigemCadastro = "G"
            };

            if (servicos.TipoCobranca == "H" || servicos.TipoCobranca == TipoCobrancaFaturamentoEnum.Horas)
            {
                payload.TempoTrabalhado = !string.IsNullOrWhiteSpace(parameters.HoraMinuto)
                    ? parameters.HoraMinuto
                    : parameters.ValorTipoCobrancaInformado;

                payload.Valor = !string.IsNullOrWhiteSpace(parameters.ValorTipoCobrancaInformado) &&
                                decimal.TryParse(parameters.ValorTipoCobrancaInformado.Replace(".", ","),
                                    out decimal valorInformadoH)
                    ? valorInformadoH
                    : servicos.PrecoPadrao;
            }
            else
            {
                payload.Valor = !string.IsNullOrWhiteSpace(parameters.ValorTipoCobrancaInformado) &&
                                decimal.TryParse(parameters.ValorTipoCobrancaInformado.Replace(".", ","),
                                    out decimal valorInformado)
                    ? valorInformado
                    : servicos.PrecoPadrao;
            }

            await _context.FaturamentoServicoGrv.AddAsync(payload, ct);

            await _context.SaveChangesAsync(ct);
            return MensagemViewHelper.SetCreateSuccess("Serviço associado com sucesso");
        }

        public async Task<MensagemDTO> UpdateServiceAssociationAsync(AtualizarServicoAoGgvParameters parameters,
            int usuarioId, CancellationToken ct = default)
        {
            if (parameters == null || parameters.IdentificadorServicoGrv <= 0)
            {
                return MensagemViewHelper.SetBadRequest("Informe o Identificador do Serviço");
            }

            MensagemDTO ResultView =
                new GrvService(_context).ValidateInputGrv(parameters.IdentificadorProcesso, usuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            FaturamentoServicoGrvModel servicoAssociado = await _context.FaturamentoServicoGrv
                .Include(x => x.FaturamentoServicoTipoVeiculo)
                    .ThenInclude(x => x.FaturamentoServicoAssociado)
                        .ThenInclude(x => x.FaturamentoServicoTipo)
                .AsTracking()
                .FirstOrDefaultAsync(
                    x => x.GrvId == parameters.IdentificadorProcesso && x.FaturamentoServicoGrvId == parameters.IdentificadorServicoGrv,
                    cancellationToken: ct);

            if (servicoAssociado is null)
            {
                return MensagemViewHelper.SetNotFound(
                    "Serviço não encontrado para alteração ou não vinculado a este Processo");
            }

            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(parameters.ValorTipoCobrancaInformado) &&
                !string.IsNullOrWhiteSpace(parameters.HoraMinuto))
            {
                parameters.ValorTipoCobrancaInformado = parameters.HoraMinuto;
            }

            var tipoCobranca = servicoAssociado.FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.FaturamentoServicoTipo?.TipoCobranca;
            decimal precoPadrao = servicoAssociado.FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.PrecoPadrao ?? 0;

            if (tipoCobranca == "H" || tipoCobranca == TipoCobrancaFaturamentoEnum.Horas)
            {
                if (string.IsNullOrWhiteSpace(parameters.HoraMinuto) &&
                    string.IsNullOrWhiteSpace(parameters.ValorTipoCobrancaInformado))
                {
                    erros.Add("Informe o Tempo trabalhado");
                }
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.SetBadRequest(erros);
            }

            servicoAssociado.QuantidadeDesconto = (parameters.Quantidade.HasValue && parameters.Quantidade.Value > 0)
                ? parameters.Quantidade.Value
                : 1;

            if (tipoCobranca == "H" || tipoCobranca == TipoCobrancaFaturamentoEnum.Horas)
            {
                servicoAssociado.TempoTrabalhado = !string.IsNullOrWhiteSpace(parameters.HoraMinuto)
                    ? parameters.HoraMinuto
                    : parameters.ValorTipoCobrancaInformado;

                servicoAssociado.Valor = !string.IsNullOrWhiteSpace(parameters.ValorTipoCobrancaInformado) &&
                                decimal.TryParse(parameters.ValorTipoCobrancaInformado.Replace(".", ","),
                                    out decimal valorInformadoH)
                    ? valorInformadoH
                    : precoPadrao;
            }
            else
            {
                servicoAssociado.Valor = !string.IsNullOrWhiteSpace(parameters.ValorTipoCobrancaInformado) &&
                                decimal.TryParse(parameters.ValorTipoCobrancaInformado.Replace(".", ","),
                                    out decimal valorInformado)
                    ? valorInformado
                    : precoPadrao;
            }

            _context.FaturamentoServicoGrv.Update(servicoAssociado);

            await _context.SaveChangesAsync(ct);
            return MensagemViewHelper.SetUpdateSuccess("Serviço associado alterado com sucesso");
        }

        public async Task<MensagemDTO> DeleteServiceAssociationAsync(int GrvId, int UsuarioId,
            int faturamentoServicoGrvId, CancellationToken ct)
        {
            if (faturamentoServicoGrvId <= 0)
            {
                return MensagemViewHelper.SetBadRequest("Informe os Identificadores dos Serviços");
            }

            MensagemDTO ResultView = new GrvService(_context).ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            FaturamentoServicoGrvModel Servico = await _context.FaturamentoServicoGrv
                .AsTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId && x.FaturamentoServicoGrvId == faturamentoServicoGrvId,
                    cancellationToken: ct);

            if (Servico is null)
            {
                return MensagemViewHelper.SetNotFound(
                    "Nenhum serviço encontrado para exclusão ou os serviços não pertencem a este Processo");
            }

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(ct))
            {
                try
                {
                    _context.FaturamentoServicoGrv.Remove(Servico);

                    await _context.SaveChangesAsync(ct);

                    await transaction.CommitAsync(ct);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(ct);

                    return MensagemViewHelper.SetInternalServerError(ex);
                }
            }

            return MensagemViewHelper.SetDeleteSuccess("Serviço(s) removido(s) com sucesso");
        }

        public async Task<DadosMestresDTO> ListDadosMestresAsync(int GrvId, int UsuarioId, CancellationToken ct)
        {
            TabelaGenericaService TabelaGenericaService = new(_context, _mapper);

            VistoriaService VistoriaService = new(_context, _mapper);

            DadosMestresDTO DadosMestres = new()
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
                    .ListServicoAssociadoTipoVeiculoAsync(GrvId, UsuarioId, ct)
            };

            return DadosMestres;
        }

        public async Task<ImageListDTO> ListFotosAsync(int GrvId, int UsuarioId)
        {
            ImageListDTO ResultView = new()
            {
                Mensagem = new GrvService(_context).ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await new BucketService(_context, _httpClientFactory)
                .DownloadFileAsync(BucketNomeTabelaOrigemEnum.FotoVeiculoGGV, GrvId);
        }

        public async Task<MensagemDTO> ValidarInformacoesPersistenciaAsync(GgvParameters GgvPersistencia,
            bool IsUpdatating)
        {
            if (GgvPersistencia == null)
            {
                return MensagemViewHelper.SetBadRequest("O Modelo está nulo");
            }

            MensagemDTO ResultView = new GrvService(_context)
                .ValidateInputGrv(GgvPersistencia.IdentificadorProcesso, GgvPersistencia.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            List<string> erros = new();

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .Include(x => x.Deposito)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GgvPersistencia.IdentificadorProcesso);

            if (Grv.StatusOperacao.StatusOperacaoId != "G" && Grv.StatusOperacao.StatusOperacaoId != "V")
            {
                erros.Add($"O Status do Processo não está apto para o cadastro do GGV. " +
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

            if (((DataHoraPorDeposito.Date - GgvPersistencia.DataHoraGuarda.Date).TotalDays) >
                (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda * 365))
            {
                if (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda == 1)
                {
                    erros.Add("A Data da Guarda não pode ser inferior a 1 ano");
                }
                else
                {
                    erros.Add("A Data da Guarda não pode ser inferior a " + Grv.Deposito.GrvLimiteMinimoDatahoraGuarda +
                              " anos");
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
                        erros.Add(
                            $"Foram indentificados {count} Identificador do Tipo do Cadastro da Foto inexistente");
                    }
                }
            }

            // if (GgvPersistencia.ListagemEquipamentoOpcional?.Count > 0)
            // {
            //     if (GgvPersistencia.ListagemEquipamentoOpcional.Where(x => x.IdentificadorEquipamentoOpcional <= 0).ToList().Count > 0)
            //     {
            //         erros.Add("Existe um ou mais Identificador do Equipamento Opcional inválido");
            //     }
            //
            //     if (GgvPersistencia.ListagemEquipamentoOpcional.Where(x => x.FlagEquipamentoAvariado == "S" && (x.IdentificadorTipoAvaria <= 0 || x.IdentificadorTipoAvaria == null)).ToList().Count > 0)
            //     {
            //         erros.Add("Existe um ou mais Identificador do Tipo de Avaria inválido");
            //     }
            // }

            if (GgvPersistencia.Vistoria != null)
            {
                if (GgvPersistencia.Vistoria.FlagVistoria == "S")
                {
                    if (GgvPersistencia.Vistoria.IdentificadorEmpresaVistoria > 0)
                    {
                        if (await _context.Empresa
                                .AsNoTracking()
                                .FirstOrDefaultAsync(w =>
                                    w.EmpresaId == GgvPersistencia.Vistoria.IdentificadorEmpresaVistoria) == null)
                        {
                            erros.Add("Identificador da Empresa inexistente");
                        }
                    }

                    if (GgvPersistencia.Vistoria.IdentificadorStatusVistoria > 0)
                    {
                        if (await _context.VistoriaStatus
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x =>
                                    x.VistoriaStatusId == GgvPersistencia.Vistoria.IdentificadorStatusVistoria) == null)
                        {
                            erros.Add("Identificador do Status da Vistoria inexistente");
                        }
                    }

                    if (await _context.VistoriaSituacaoChassi
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.VistoriaSituacaoChassiId == GgvPersistencia.Vistoria.IdentificadorSituacaoChassi) ==
                        null)
                    {
                        erros.Add("Identificador da Situação do Chassi inexistente");
                    }

                    if (GgvPersistencia.Vistoria.IdentificadorTipoDirecao > 0)
                    {
                        if (await _context.TabelaGenerica
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Codigo == "VISTORIA_TIPO_DIRECAO"
                                                          && x.TabelaGenericaId == GgvPersistencia.Vistoria
                                                              .IdentificadorTipoDirecao) == null)
                        {
                            erros.Add("Identificador do Tipo de Direção inexistente");
                        }
                    }

                    if (await _context.TabelaGenerica
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Codigo == "VISTORIA_ESTADO_GERAL_VEICULO"
                                                      && x.TabelaGenericaId == GgvPersistencia.Vistoria
                                                          .IdentificadorEstadoGeralVeiculo) == null)
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

            if (!IsUpdatating)
            {
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

                        List<FaturamentoServicoTipoVeiculoModel> FaturamentoServicoTipoVeiculoList = await _context
                            .FaturamentoServicoTipoVeiculo
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
                                .Where(x => x.GrvId == GgvPersistencia.IdentificadorProcesso)
                                .AsNoTracking()
                                .ToList();

                            foreach (FaturamentoServicoGrvParameters item in GgvPersistencia
                                         .ListagemFaturamentoServicoGrv)
                            {
                                if (FaturamentoServicoGrvList?.Count > 0)
                                {
                                    FaturamentoServicoGrv = FaturamentoServicoGrvList
                                        .FirstOrDefault(x =>
                                            x.FaturamentoServicoTipoVeiculoId ==
                                            item.IdentificadorServicoAssociadoTipoVeiculo);

                                    if (FaturamentoServicoGrv != null)
                                    {
                                        erros.Add(
                                            $"O Serviço {FaturamentoServicoGrv.FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.Descricao} já está cadastrado para este Processo");
                                    }
                                }
                                else
                                {
                                    FaturamentoServicoTipoVeiculo = FaturamentoServicoTipoVeiculoList
                                        .FirstOrDefault(x =>
                                            x.FaturamentoServicoTipoVeiculoId ==
                                            item.IdentificadorServicoAssociadoTipoVeiculo);

                                    if (FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.FaturamentoServicoTipo
                                            .FlagCobrarTelaGrv == "N")
                                    {
                                        erros.Add(
                                            $"Foi identificado um Serviço que não pode ser cobrado antes do Atendimento. Serviço informado: {FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.Descricao}");
                                    }
                                    else
                                    {
                                        if (string.IsNullOrWhiteSpace(item.ValorTipoCobrancaInformado) &&
                                            !string.IsNullOrWhiteSpace(item.HoraMinuto))
                                        {
                                            item.ValorTipoCobrancaInformado = item.HoraMinuto;
                                        }

                                        if (item.ValorTipoCobrancaInformado != null)
                                        {
                                            item.ValorTipoCobrancaInformado =
                                                item.ValorTipoCobrancaInformado.Replace(".", ",");
                                        }

                                        switch (FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado
                                                    .FaturamentoServicoTipo.TipoCobranca)
                                        {
                                            // Diárias
                                            case "D":

                                                if (!NumberHelper.IsNumber(
                                                        item.ValorTipoCobrancaInformado.RemoveStrings(
                                                            new[] { ".", "," })))
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Diárias inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }
                                                else if (!decimal.TryParse(item.ValorTipoCobrancaInformado,
                                                             out decimal valorDiaria) || valorDiaria < 0)
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Diárias inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }

                                                break;

                                            // Horas
                                            case "H":

                                                string tempoHoras = !string.IsNullOrWhiteSpace(item.HoraMinuto)
                                                    ? item.HoraMinuto
                                                    : item.ValorTipoCobrancaInformado;

                                                if (string.IsNullOrWhiteSpace(tempoHoras) ||
                                                    !NumberHelper.IsNumber(tempoHoras.RemoveString(":")))
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Horas inválido. Valor informado: {tempoHoras ?? string.Empty}. Informe no padrão HH:MM");
                                                }
                                                else
                                                {
                                                    string[] aux = tempoHoras.Split(':');

                                                    if (aux.Length != 2)
                                                    {
                                                        erros.Add(
                                                            $"Valor do Tipo de Cobrança Horas inválido. Valor informado: {tempoHoras}. Informe no padrão HH:MM");
                                                    }
                                                    else if (!int.TryParse(aux[0], out int horas) ||
                                                             !int.TryParse(aux[1], out int minutos) ||
                                                             horas < 0 || horas > 23 || minutos < 0 || minutos > 59)
                                                    {
                                                        erros.Add(
                                                            $"Valor do Tipo de Cobrança Horas inválido. Valor informado: {tempoHoras}. Informe no padrão HH:MM");
                                                    }
                                                    else if (horas == 0 && minutos == 0)
                                                    {
                                                        erros.Add($"Informe o Tempo trabalhado");
                                                    }
                                                }

                                                break;

                                            // Percentual
                                            case "P":

                                                if (!NumberHelper.IsNumber(
                                                        item.ValorTipoCobrancaInformado.RemoveStrings(
                                                            new[] { ".", "," })))
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Percentual inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }
                                                else if (!decimal.TryParse(item.ValorTipoCobrancaInformado,
                                                             out decimal valorPercentual) || valorPercentual < 0 ||
                                                         valorPercentual > 100)
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Percentual inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }

                                                break;

                                            // Quantidade
                                            case "Q":

                                                if (!NumberHelper.IsNumber(
                                                        item.ValorTipoCobrancaInformado.RemoveStrings(
                                                            new[] { ".", "," })))
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Quantidade inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }
                                                else if (!decimal.TryParse(item.ValorTipoCobrancaInformado,
                                                             out decimal valorQuantidade) || valorQuantidade < 0)
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Quantidade inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }

                                                break;

                                            // Tempo/Espaço
                                            case "T":

                                                if (!NumberHelper.IsNumber(
                                                        item.ValorTipoCobrancaInformado.RemoveStrings(
                                                            new[] { ".", "," })))
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Tempo inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }
                                                else if (!decimal.TryParse(item.ValorTipoCobrancaInformado,
                                                             out decimal valorTempo) || valorTempo < 0)
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Tempo inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }

                                                break;

                                            // Valor Monetário
                                            case "V":

                                                if (!NumberHelper.IsNumber(
                                                        item.ValorTipoCobrancaInformado.RemoveStrings(
                                                            new[] { ".", "," })))
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Valor Monetário inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }
                                                else if (!decimal.TryParse(item.ValorTipoCobrancaInformado, out _))
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Valor Monetário inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }
                                                else if (decimal.Parse(item.ValorTipoCobrancaInformado) < 0)
                                                {
                                                    erros.Add(
                                                        $"Valor do Tipo de Cobrança Valor Monetário inválido. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                }
                                                else
                                                {
                                                    if (FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado
                                                            .FlagPermiteAlteracaoValor == "N"
                                                        && FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado
                                                            .PrecoPadrao !=
                                                        decimal.Parse(item.ValorTipoCobrancaInformado))
                                                    {
                                                        erros.Add(
                                                            $"Valor do Tipo de Cobrança Valor Monetário não pode ser diferente do Preço Padrão. Valor informado: {item.ValorTipoCobrancaInformado}");
                                                    }
                                                    else if (decimal.Parse(item.ValorTipoCobrancaInformado) <
                                                             FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado
                                                                 .PrecoValorMinimo)
                                                    {
                                                        erros.Add(
                                                            $"Valor do Tipo de Cobrança Valor Monetário não pode ser menor do que o valor do Preço Mínimo parametrizado" +
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
            }

            return erros.Count == 0 ? MensagemViewHelper.SetOk() : MensagemViewHelper.SetBadRequest(erros);
        }
    }
}
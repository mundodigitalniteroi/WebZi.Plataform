using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Runtime.CompilerServices;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.WebServices.DetranRio;
using WebZi.Plataform.Domain.Views.Faturamento;
using WebZi.Plataform.Domain.Views.Localizacao;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class FaturamentoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public FaturamentoService(AppDbContext context)
        {
            _context = context;
        }

        public FaturamentoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FaturamentoViewModel> SimularAsync(string PlacaChassi, int IdentificadorCliente, int IdentificadorDeposito)
        {
            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(IdentificadorDeposito);

            DetranRioVeiculoViewModel DetranRioVeiculo = new();

            if (PlacaChassi.IsPlaca())
            {
                DetranRioVeiculo = await new DetranRioService(_context, _mapper).GetViewByPlacaAsync(PlacaChassi);
            }
            else
            {
                DetranRioVeiculo = await new DetranRioService(_context, _mapper).GetViewByChassiAsync(PlacaChassi);
            }

            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento = new()
            {
                Grv = new()
                {
                    FaturamentoProdutoId = "DEP",

                    ClienteId = IdentificadorCliente,

                    DepositoId = IdentificadorDeposito,

                    TipoVeiculoId = DetranRioVeiculo.TipoVeiculo.IdentificadorTipoVeiculo,

                    DataHoraGuarda = DataHoraPorDeposito,

                    FlagComboio = "S",

                    StatusOperacaoId = "V" // GGV
                },

                ClienteDeposito = await _context.ClienteDeposito
                    .Include(x => x.Cliente)
                    .Include(x => x.Deposito)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ClienteId == IdentificadorCliente && x.DepositoId == IdentificadorDeposito),

                DataHoraPorDeposito = DataHoraPorDeposito,

                TiposMeiosCobrancas = await _context.TipoMeioCobranca
                    .AsNoTracking()
                    .ToListAsync(),

                FaturamentoRegras = await _context.FaturamentoRegra
                    .Include(x => x.FaturamentoRegraTipo)
                    .Where(x => x.ClienteId == IdentificadorCliente && x.DepositoId == IdentificadorDeposito)
                    .AsNoTracking()
                    .ToListAsync()
            };

            ParametrosCalculoFaturamento.Cliente = ParametrosCalculoFaturamento.ClienteDeposito.Cliente;

            ParametrosCalculoFaturamento.Deposito = ParametrosCalculoFaturamento.ClienteDeposito.Deposito;

            ParametrosCalculoFaturamento.Grv.Cliente = ParametrosCalculoFaturamento.ClienteDeposito.Cliente;

            ParametrosCalculoFaturamento.Grv.Deposito = ParametrosCalculoFaturamento.ClienteDeposito.Deposito;

            FaturamentoViewModel ResultView = new();

            var Faturamento = Faturar(ParametrosCalculoFaturamento, true, true);

            return ResultView;
        }

        public FaturamentoModel Faturar(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, bool simular = false, bool faturarSemGrv = false)
        {
            if (ParametrosCalculoFaturamento.DataLiberacao == DateTime.MinValue)
            {
                ParametrosCalculoFaturamento.DataLiberacao = ParametrosCalculoFaturamento.DataHoraPorDeposito;
            }

            #region Selecionar os Serviços cadastrados no GRV
            List<ViewFaturamentoServicoGrvModel> FaturamentoServicosGrvs = new();

            if (!faturarSemGrv)
            {
                _context.ViewFaturamentoServicoGrv
                    .Where(x => x.GrvId == ParametrosCalculoFaturamento.Grv.GrvId &&
                                x.FaturamentoProdutoId == ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId &&
                                x.FlagTributacao == "N")
                    .AsNoTracking()
                    .ToList();

                if (FaturamentoServicosGrvs?.Count == 0)
                {
                    throw new Exception("Não foi encontrado Serviço cadastrado para este GRV");
                }
            }
            #endregion Selecionar os Serviços cadastrados no GRV

            #region Selecionar todos os Serviços associados ao CLIDEP, incluindo os com a Vigência finalizada
            List<ViewFaturamentoServicoAssociadoVeiculoModel> FaturamentoServicosAssociadosVeiculos = _context.ViewFaturamentoServicoAssociadoVeiculo
                .Where(x => x.ClienteId == ParametrosCalculoFaturamento.Grv.ClienteId &&
                            x.DepositoId == ParametrosCalculoFaturamento.Grv.DepositoId &&
                            x.TipoVeiculoId == ParametrosCalculoFaturamento.Grv.TipoVeiculoId &&
                            x.FaturamentoProdutoId == ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId)
                .AsNoTracking()
                .ToList();

            if (FaturamentoServicosAssociadosVeiculos?.Count == 0)
            {
                throw new Exception("Não foi encontrado Serviço associado ao Cliente + Depósito + Tipo de Veículo");
            }
            #endregion Selecionar todos os Serviços associados ao CLIDEP, incluindo os com a Vigência finalizada

            #region Verificação de Faturamentos anteriores
            // Faturamento.Status:
            // N = Novo Faturamento/Não Pago;
            // A = Faturamento Adicional e Não Pago (Pra quando a Fatura foi paga em atraso);
            // C = Cancelado, pra quando foi gerada uma Fatura para uma Fatura Vencida e que não foi paga;
            // P = Fatura Paga.

            // Se existir ao menos 1 Fatura paga, não deve dar Desconto
            if (!faturarSemGrv)
            {
                if (_context.Faturamento
                    .Where(x => x.AtendimentoId == ParametrosCalculoFaturamento.Atendimento.AtendimentoId
                             && x.Status == "P")
                    .AsNoTracking()
                    .Any())
                {
                    // Faturamentos adicionais não podem receber descontos
                    ParametrosCalculoFaturamento.FaturamentoAdicional = true;
                }
            }

            // Consulta da última Fatura para cancelar
            FaturamentoModel UltimoFaturamento = null;

            if (!faturarSemGrv)
            {
                UltimoFaturamento = _context.Faturamento
                    .OrderByDescending(x => x.FaturamentoId)
                    .FirstOrDefault(x => x.AtendimentoId == ParametrosCalculoFaturamento.Atendimento.AtendimentoId);

                if (UltimoFaturamento != null)
                {
                    #region Cancelar o Faturamento atual
                    UltimoFaturamento.UsuarioAlteracaoId = ParametrosCalculoFaturamento.Atendimento.UsuarioCadastroId;

                    UltimoFaturamento.Status = "C";

                    UltimoFaturamento.DataAlteracao = ParametrosCalculoFaturamento.DataHoraPorDeposito;

                    if (ParametrosCalculoFaturamento.FlagCadastrarFaturamento)
                    {
                        _context.Faturamento.Update(UltimoFaturamento);
                    }

                    if (ParametrosCalculoFaturamento.TipoMeioCobranca.TipoMeioCobrancaId == 0)
                    {
                        ParametrosCalculoFaturamento.TipoMeioCobranca.TipoMeioCobrancaId = UltimoFaturamento.TipoMeioCobrancaId;
                    }
                    #endregion Cancelar o Faturamento atual

                    // Se a Fatura for Nova, então está sendo cancelada e gerada uma nova, incluindo a aplicação dos Descontos caso houver.
                    if (UltimoFaturamento.Status == "A" || ParametrosCalculoFaturamento.FaturamentoAdicional)
                    {
                        ParametrosCalculoFaturamento.FlagFaturamentoCompleto = false;
                    }
                }
            }
            #endregion Verificação de Faturamentos anteriores

            #region Cálculo das Diárias
            CalculoDiariasModel CalculoDiarias = new();

            if (ParametrosCalculoFaturamento.Diarias == 0)
            {
                CalculoDiarias = new CalculoDiariasService(_context)
                    .Calcular(ParametrosCalculoFaturamento);
            }
            else
            {
                CalculoDiarias.Diarias = ParametrosCalculoFaturamento.Diarias;
            }

            ParametrosCalculoFaturamento.DataHoraInicialParaCalculo = CalculoDiarias.DataHoraInicialParaCalculo;
            #endregion Cálculo das Diárias

            #region Composição do Faturamento
            CalculoFaturamentoQuantidadeAlteradaModel FaturamentoQuantidadeAlterada = new();

            List<FaturamentoComposicaoModel> FaturamentoComposicoes = new();

            FaturamentoComposicaoModel FaturamentoComposicao = new();

            ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculo = new();

            List<ViewFaturamentoServicoAssociadoVeiculoModel> FaturamentoServicosAssociadosVeiculosTodasVigenciasEncontradas = new();

            decimal ValorFaturado = 0;
            int DiariasCalculadas = 0;
            int Horas = 0;

            foreach (ViewFaturamentoServicoGrvModel FaturamentoServicoGrv in FaturamentoServicosGrvs)
            {
                if (!CheckServicoDeveSerCalculado(FaturamentoServicoGrv, UltimoFaturamento, ParametrosCalculoFaturamento))
                {
                    continue;
                }

                FaturamentoComposicao = new()
                {
                    FaturamentoServicoTipoVeiculoId = FaturamentoServicoGrv.FaturamentoServicoTipoVeiculoId,

                    TipoComposicao = FaturamentoServicoGrv.TipoCobranca,

                    ValorTipoComposicao = FaturamentoServicoGrv.PrecoPadrao,
                };

                // DIÁRIAS
                if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Diárias)
                {
                    // Forma de Cobrança:
                    // AM: Ambos;
                    // VA: Vigência Atual (Valor Padrão);
                    // VI: Vigência Inicial.

                    // Quantidade Alterada só se aplica às Diárias
                    FaturamentoQuantidadeAlterada = null;

                    if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas?.Count > 0)
                    {
                        FaturamentoQuantidadeAlterada = ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas
                            .Find(w => w.FaturamentoServicoTipoVeiculoId == FaturamentoServicoGrv.FaturamentoServicoTipoVeiculoId);
                    }

                    if (new[] { "AM", "VI" }.Contains(FaturamentoServicoGrv.FormaCobranca))
                    {
                        DiariasCalculadas = CalculoDiarias.Diarias;

                        if (FaturamentoServicoGrv.FormaCobranca == "AM")
                        {
                            // Primeiro filtro, cobrar por todas as vigências encontradas
                            FaturamentoServicosAssociadosVeiculosTodasVigenciasEncontradas = FaturamentoServicosAssociadosVeiculos
                                .Where(x => (x.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId &&
                                           (ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Value.Date >= x.DataVigenciaInicial && ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Value.Date <= x.DataVigenciaFinal)) ||
                                            ParametrosCalculoFaturamento.Grv.DataHoraGuarda <= x.DataVigenciaFinal || x.DataVigenciaFinal == null)
                                .ToList();

                            foreach (ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculoAmbos in FaturamentoServicosAssociadosVeiculosTodasVigenciasEncontradas)
                            {
                                // Retorna a quantidade de Dias entre as datas
                                CalculoDiarias.Diarias = GetQuantidadeDiasServicoDiarias(FaturamentoServicoAssociadoVeiculoAmbos, ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Value, ParametrosCalculoFaturamento.DataHoraPorDeposito);

                                if (CalculoDiarias.Diarias >= DiariasCalculadas)
                                {
                                    CalculoDiarias.Diarias = DiariasCalculadas;

                                    DiariasCalculadas = 0;
                                }
                                else
                                {
                                    DiariasCalculadas -= CalculoDiarias.Diarias;
                                }

                                FaturamentoComposicao.FaturamentoServicoTipoVeiculoId = FaturamentoServicoAssociadoVeiculoAmbos.FaturamentoServicoTipoVeiculoId;

                                FaturamentoComposicao.TipoComposicao = FaturamentoServicoAssociadoVeiculoAmbos.TipoCobranca;

                                FaturamentoComposicao.ValorTipoComposicao = FaturamentoServicoAssociadoVeiculoAmbos.PrecoPadrao;

                                // Aplicar Quantidade Alterada
                                if (FaturamentoQuantidadeAlterada != null)
                                {
                                    FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao, FaturamentoQuantidadeAlterada);

                                    CalculoDiarias.Diarias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                                }

                                FaturamentoComposicao.QuantidadeComposicao = CalculoDiarias.Diarias;

                                FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoAssociadoVeiculoAmbos.PrecoPadrao * CalculoDiarias.Diarias, 2);

                                FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;

                                // Aplicar os Descontos
                                if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoDescontos?.Count > 0)
                                {
                                    FaturamentoComposicao = AplicarDesconto(FaturamentoComposicao, ParametrosCalculoFaturamento.FaturamentoDescontos);
                                }

                                FaturamentoComposicao.TipoLancamento = TipoLancamentoFaturamentoEnum.Crédito;

                                ValorFaturado += FaturamentoComposicao.ValorFaturado;

                                FaturamentoComposicoes.Add(FaturamentoComposicao);

                                if (DiariasCalculadas == 0)
                                {
                                    break;
                                }
                            }

                            continue;
                        }
                        else if (FaturamentoServicoGrv.FormaCobranca == "VI")
                        {
                            // Segundo filtro, cobrar pela Vigência Inicial
                            FaturamentoServicoAssociadoVeiculo = FaturamentoServicosAssociadosVeiculos
                                .OrderBy(x => x.DataVigenciaInicial)
                                .FirstOrDefault(x => x.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId
                                    && (x.DataVigenciaFinal >= ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Value.Date || x.DataVigenciaFinal == null));

                            // Aplicar Quantidade Alterada
                            if (FaturamentoQuantidadeAlterada != null)
                            {
                                FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao, FaturamentoQuantidadeAlterada);

                                CalculoDiarias.Diarias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                            }

                            FaturamentoComposicao.TipoComposicao = FaturamentoServicoAssociadoVeiculo.TipoCobranca;

                            FaturamentoComposicao.FaturamentoServicoTipoVeiculoId = FaturamentoServicoAssociadoVeiculo.FaturamentoServicoTipoVeiculoId;

                            FaturamentoComposicao.ValorTipoComposicao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;

                            FaturamentoComposicao.QuantidadeComposicao = CalculoDiarias.Diarias;

                            FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoComposicao.ValorTipoComposicao * CalculoDiarias.Diarias, 2);

                            FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                        }
                    }
                    else
                    {
                        // Aplicar Quantidade Alterada
                        if (FaturamentoQuantidadeAlterada != null)
                        {
                            FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao, FaturamentoQuantidadeAlterada);

                            CalculoDiarias.Diarias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                        }

                        FaturamentoComposicao.QuantidadeComposicao = CalculoDiarias.Diarias;

                        FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * CalculoDiarias.Diarias, 2);

                        FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                    }
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Horas)
                {
                    if (string.IsNullOrWhiteSpace(FaturamentoServicoGrv.TempoTrabalhado))
                    {
                        continue;
                    }

                    FaturamentoComposicao.QuantidadeComposicao = Math.Round(Convert.ToDecimal(TimeSpan.Parse(FaturamentoServicoGrv.TempoTrabalhado).TotalHours), 2);

                    FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * FaturamentoComposicao.QuantidadeComposicao.Value, 2);

                    FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Quantidade)
                {
                    if (FaturamentoServicoGrv.FlagRebocada == "S")
                    {
                        FaturamentoComposicao.QuantidadeComposicao = 1;

                        if (FaturamentoServicoGrv.FormaCobranca == "VI") // VI: Vigência Inicial
                        {
                            FaturamentoServicoAssociadoVeiculo = FaturamentoServicosAssociadosVeiculos
                                .OrderBy(x => x.DataVigenciaInicial)
                                .FirstOrDefault(x => x.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId
                                                 && (x.DataVigenciaFinal >= ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Value.Date || x.DataVigenciaFinal == null));

                            FaturamentoServicoGrv.PrecoPadrao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;

                            FaturamentoComposicao.FaturamentoServicoTipoVeiculoId = FaturamentoServicoAssociadoVeiculo.FaturamentoServicoTipoVeiculoId;

                            FaturamentoComposicao.ValorTipoComposicao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;
                        }
                    }
                    else
                    {
                        FaturamentoComposicao.QuantidadeComposicao = Math.Round(FaturamentoServicoGrv.Valor.Value, 2);
                    }

                    if (FaturamentoComposicao.QuantidadeComposicao == 0)
                    {
                        continue;
                    }

                    FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * Math.Round(FaturamentoComposicao.QuantidadeComposicao.Value, 2), 2);

                    FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Valor)
                {
                    if (FaturamentoServicoGrv.FlagPermiteAlteracaoValor == "N" && (FaturamentoServicoGrv.PrecoPadrao > 0) && (FaturamentoServicoGrv.Valor == 0))
                    {
                        FaturamentoServicoGrv.Valor = 1;
                    }

                    if (FaturamentoServicoGrv.FlagRebocada == "S")
                    {
                        FaturamentoComposicao.QuantidadeComposicao = 1;

                        FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.Valor.Value, 2);

                        FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                    }
                    else
                    {
                        FaturamentoComposicao.QuantidadeComposicao = Math.Round(FaturamentoServicoGrv.Valor.Value, 2);

                        FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * FaturamentoServicoGrv.Valor.Value, 2);

                        FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                    }
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Tempo)
                {
                    if ((Horas = (int)(ParametrosCalculoFaturamento.DataHoraPorDeposito - ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Value).TotalHours) == 0)
                    {
                        Horas++;
                    }

                    FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * Horas, 2);

                    FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                }

                // Aplicar os Descontos
                if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoDescontos?.Count > 0)
                {
                    FaturamentoComposicao = AplicarDesconto(FaturamentoComposicao, ParametrosCalculoFaturamento.FaturamentoDescontos);
                }

                FaturamentoComposicao.TipoLancamento = TipoLancamentoFaturamentoEnum.Crédito;

                ValorFaturado += FaturamentoComposicao.ValorFaturado;

                FaturamentoComposicoes.Add(FaturamentoComposicao);
            }
            #endregion Composição do Faturamento

            #region Tributação
            if (ValorFaturado > 0)
            {
                List<CalculoTributacaoModel> Tributacoes = CalcularTributacao(_context,
                    ParametrosCalculoFaturamento,
                    ValorFaturado,
                    ParametrosCalculoFaturamento.Atendimento.NotaFiscalDocumento,
                    ParametrosCalculoFaturamento.Atendimento.NotaFiscalMunicipio,
                    ParametrosCalculoFaturamento.Atendimento.NotaFiscalUF);

                if (Tributacoes != null)
                {
                    foreach (CalculoTributacaoModel Tributacao in Tributacoes)
                    {
                        FaturamentoComposicoes.Add(Tributacao);

                        ValorFaturado += Tributacao.ValorFaturado;
                    }
                }
            }
            #endregion Tributação

            #region Cadastro do Faturamento
            FaturamentoModel Faturamento = new()
            {
                AtendimentoId = ParametrosCalculoFaturamento.Atendimento.AtendimentoId,

                UsuarioCadastroId = ParametrosCalculoFaturamento.UsuarioCadastroId,

                TipoMeioCobrancaId = ParametrosCalculoFaturamento.TipoMeioCobranca.TipoMeioCobrancaId,

                HoraDiaria = CalculoDiarias.HoraDiaria,

                MaximoDiariasParaCobranca = CalculoDiarias.MaximoDiariasParaCobranca,

                MaximoDiasVencimento = CalculoDiarias.MaximoDiasVencimento,

                FlagUsarHoraDiaria = CalculoDiarias.FlagUsarHoraDiaria,

                FlagClienteRealizaFaturamentoArrecadacao = CalculoDiarias.FlagClienteRealizaFaturamentoArrecadacao,

                FlagCobrarDiariasDiasCorridos = CalculoDiarias.FlagCobrarDiariasDiasCorridos,

                DataCalculo = CalculoDiarias.DataHoraInicialParaCalculo,

                DataVencimento = CalcularDataVencimento(ParametrosCalculoFaturamento, CalculoDiarias, ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento ? ParametrosCalculoFaturamento.DataHoraPorDeposito : ParametrosCalculoFaturamento.DataLiberacao),

                DataCadastro = ParametrosCalculoFaturamento.DataHoraPorDeposito,

                ValorFaturado = ValorFaturado,

                FaturamentoComposicoes = FaturamentoComposicoes
            };

            if (ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento)
            {
                Faturamento.DataRetroativa = ParametrosCalculoFaturamento.DataLiberacao.Date;

                Faturamento.FlagPermissaoDataRetroativaFaturamento = "S";
            }

            if (UltimoFaturamento != null)
            {
                Faturamento.Sequencia += UltimoFaturamento.Sequencia;
            }

            Faturamento.NumeroIdentificacao = CreateNumeroIdentificacao(ParametrosCalculoFaturamento, Faturamento.Sequencia);

            if (ParametrosCalculoFaturamento.FlagCadastrarFaturamento)
            {
                // _context.SetUserContextInfo(Faturamento.UsuarioCadastroId);

                _context.Faturamento.Add(Faturamento);
            }

            return Faturamento;
            #endregion Cadastro do Faturamento
        }

        private static bool CheckServicoDeveSerCalculado(ViewFaturamentoServicoGrvModel FaturamentoServicoGrv, FaturamentoModel UltimoFaturamento, CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId != "DEP" &&
                ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId != "DRF" &&
                FaturamentoServicoGrv.FaturamentoServicoGrvId == 0)
            {
                return false;
            }
            else if (FaturamentoServicoGrv.FlagPermiteAlteracaoValor == "S" &&
                     FaturamentoServicoGrv.Valor <= 0)
            {
                return false;
            }
            else if (FaturamentoServicoGrv.FlagRealizarCobranca == "N")
            {
                // Se o Usuário escolheu por não cobrar o Serviço
                return false;
            }
            else if (!ParametrosCalculoFaturamento.FlagFaturamentoCompleto &&
                     FaturamentoServicoGrv.FlagCobrarSomentePrimeiraFatura == "S")
            {
                // Se não for o primeiro Faturamento e se o Serviço for para ser cobrado apenas no primeiro Faturamento
                return false;
            }
            else if (FaturamentoServicoGrv.FlagCobrarSomentePrimeiraFatura == "S" &&
                     UltimoFaturamento != null &&
                     UltimoFaturamento.Status == "P")
            {
                // Se a última Fatura for paga e o Serviço só pode ser cobrado na primeira Fatura
                return false;
            }
            else if (ParametrosCalculoFaturamento.Grv.FlagComboio == "S" &&
                     FaturamentoServicoGrv.FlagRebocada == "S")
            {
                // Não cobrar rebocada caso o veículo entrou no Depósito por Comboio
                return false;
            }
            else if (FaturamentoServicoGrv.FaturamentoRegraTipoCodigo != null &&
                     FaturamentoServicoGrv.FaturamentoRegraTipoCodigo == FaturamentoRegraTipoEnum.CobrarTarifaBancaria &&
                     !ParametrosCalculoFaturamento.TipoMeioCobranca.TipoMeioCobrancaId.Equals(1))
            {
                // Se o serviço tiver a regra "Cobrança de Tarifa Bancária" e se o Tipo do Meio de Cobrança for Boleto
                return false;
            }

            return true;
        }

        private static int GetQuantidadeDiasServicoDiarias(ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculo, DateTime DataHoraGuarda, DateTime DataHoraAtualPorDeposito)
        {
            DateTime DataInicial = DataHoraGuarda;

            DateTime DataFinal = DataHoraAtualPorDeposito;

            if (FaturamentoServicoAssociadoVeiculo.DataVigenciaInicial > DataHoraGuarda)
            {
                DataInicial = FaturamentoServicoAssociadoVeiculo.DataVigenciaInicial;
            }

            // Se data final da vigência for menor que a data atual
            if ((FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal > DateTime.MinValue) &&
                (FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Date < DataHoraAtualPorDeposito.Date))
            {
                DataFinal = new DateTime(FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Year, FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Month, FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Day, 23, 59, 59);
            }

            return 1 + DateTimeHelper.GetDaysBetweenTwoDates(DataInicial, DataFinal);
        }

        private static FaturamentoComposicaoModel AplicarQuantidadeAlterada(FaturamentoComposicaoModel FaturamentoComposicao, CalculoFaturamentoQuantidadeAlteradaModel FaturamentoQuantidadeAlterada)
        {
            FaturamentoComposicao.UsuarioAlteracaoQuantidadeId = FaturamentoQuantidadeAlterada.UsuarioAlteracaoQuantidadeId;

            FaturamentoComposicao.QuantidadeAlterada = FaturamentoQuantidadeAlterada.QuantidadeAlterada;

            FaturamentoComposicao.ObservacaoQuantidadeAlterada = FaturamentoQuantidadeAlterada.ObservacaoQuantidadeAlterada;

            return FaturamentoComposicao;
        }

        private static FaturamentoComposicaoModel AplicarDesconto(FaturamentoComposicaoModel FaturamentoComposicao, List<CalculoFaturamentoDescontoModel> ListFaturamentoDesconto)
        {
            if (ListFaturamentoDesconto != null)
            {
                CalculoFaturamentoDescontoModel FaturamentoDesconto = ListFaturamentoDesconto
                    .Find(w => w.FaturamentoServicoTipoVeiculoId == FaturamentoComposicao.FaturamentoServicoTipoVeiculoId);

                if (FaturamentoDesconto != null)
                {
                    FaturamentoComposicao.UsuarioDescontoId = FaturamentoDesconto.UsuarioDescontoId;

                    FaturamentoComposicao.TipoDesconto = FaturamentoDesconto.TipoDesconto;

                    FaturamentoComposicao.ValorDesconto = FaturamentoDesconto.ValorDesconto;

                    FaturamentoComposicao.QuantidadeDesconto = FaturamentoDesconto.QuantidadeDesconto;

                    FaturamentoComposicao.ObservacaoDesconto = FaturamentoDesconto.ObservacaoDesconto;

                    if (FaturamentoComposicao.TipoDesconto == TipoDescontoFaturamentoEnum.Valor)
                    {
                        FaturamentoComposicao.ValorFaturado = Math.Round(FaturamentoComposicao.ValorComposicao - FaturamentoDesconto.ValorDesconto, 2);
                    }
                    else
                    {
                        FaturamentoComposicao.ValorFaturado = Math.Round(NumberHelper.GetPercentage(FaturamentoComposicao.ValorComposicao, FaturamentoDesconto.QuantidadeDesconto), 2);
                    }
                }
            }

            return FaturamentoComposicao;
        }

        private static List<CalculoTributacaoModel> CalcularTributacao(AppDbContext _context, CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, decimal valorCalculado, string notaFiscalCnpj, string notaFiscalMunicipio, string notaFiscalUF)
        {
            if (valorCalculado <= 0 && string.IsNullOrWhiteSpace(notaFiscalCnpj) || string.IsNullOrWhiteSpace(notaFiscalMunicipio) || string.IsNullOrWhiteSpace(notaFiscalUF))
            {
                return null;
            }

            List<ViewFaturamentoServicoAssociadoVeiculoModel> ServicosTributados = _context.ViewFaturamentoServicoAssociadoVeiculo
                .Where(x => x.ClienteId == ParametrosCalculoFaturamento.Grv.ClienteId
                    && x.DepositoId == ParametrosCalculoFaturamento.Grv.DepositoId
                    && x.TipoVeiculoId == ParametrosCalculoFaturamento.Grv.TipoVeiculoId
                    && x.FaturamentoProdutoId == ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId
                    && x.FlagTributacao == "S"
                    && x.DataVigenciaFinal == null)
                .AsNoTracking()
                .ToList();

            if (ServicosTributados == null)
            {
                return null;
            }

            ViewEnderecoCompletoModel Endereco = _context.EnderecoCompleto
                .AsNoTracking()
                .FirstOrDefault(x => x.CEPId == ParametrosCalculoFaturamento.Grv.Deposito.CEPId);

            if (Endereco == null)
            {
                return null;
            }

            if (StringHelper.Normalize(notaFiscalMunicipio) != StringHelper.Normalize(Endereco.Municipio) || notaFiscalUF != Endereco.UF)
            {
                return null;
            }

            #region Selecionar Regras do Faturamento
            FaturamentoRegraModel FaturamentoRegra = _context.FaturamentoRegra
                .Include(x => x.FaturamentoRegraTipo)
                .Where(x => x.ClienteId == ParametrosCalculoFaturamento.Grv.ClienteId
                         && x.DepositoId == ParametrosCalculoFaturamento.Grv.DepositoId
                         && x.FaturamentoRegraTipo.Codigo == FaturamentoRegraTipoEnum.DescontoISS)
                .AsNoTracking()
                .FirstOrDefault();

            if (FaturamentoRegra != null && Convert.ToDecimal(FaturamentoRegra.Valor) > valorCalculado)
            {
                return null;
            }
            #endregion Selecionar Regras do Faturamento

            List<CalculoTributacaoModel> Tributacoes = new();

            CalculoTributacaoModel Tributacao;

            foreach (ViewFaturamentoServicoAssociadoVeiculoModel item in ServicosTributados)
            {
                if (item.FaturamentoRegraTipoCodigo == FaturamentoRegraTipoEnum.DescontoISS)
                {
                    continue;
                }

                Tributacao = new CalculoTributacaoModel
                {
                    FaturamentoServicoTipoVeiculoId = item.FaturamentoServicoTipoVeiculoId,

                    FaturamentoServicoAssociadoId = item.FaturamentoServicoAssociadoId,

                    TipoComposicao = item.TipoCobranca,

                    TipoLancamento = TipoLancamentoFaturamentoEnum.Débito,

                    QuantidadeComposicao = 1,

                    ValorTipoComposicao = item.PrecoPadrao
                };

                if (Tributacao.TipoComposicao == "P")
                {
                    Tributacao.ValorComposicao = valorCalculado * (item.PrecoPadrao / 100);

                    Tributacao.ValorFaturado = Tributacao.ValorComposicao;
                }
                else if (Tributacao.TipoComposicao == "V")
                {
                    Tributacao.ValorComposicao = item.PrecoPadrao;

                    Tributacao.ValorFaturado = Tributacao.ValorComposicao;
                }

                Tributacao.ValorComposicao *= -1;

                Tributacao.ValorFaturado *= -1;

                Tributacoes.Add(Tributacao);
            }

            return Tributacoes;
        }

        private static string CreateNumeroIdentificacao(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, int Sequencia)
        {
            return StringHelper.AddCharToLeft(ParametrosCalculoFaturamento.Grv.NumeroFormularioGrv, '0', 9) +
                   StringHelper.AddCharToLeft(ParametrosCalculoFaturamento.Grv.DepositoId.ToString(), '0', 4) +
                   StringHelper.AddCharToLeft(Sequencia.ToString(), '0', 3);
        }

        private static DateTime CalcularDataVencimento(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, CalculoDiariasModel CalculoDiarias, DateTime? dataFinal = null)
        {
            if (!dataFinal.HasValue)
            {
                dataFinal = ParametrosCalculoFaturamento.DataHoraPorDeposito;
            }

            DateTime dataVencimento = dataFinal.Value;

            dataVencimento = dataVencimento.SetTime(23, 59, 59);

            // Se for pra cobrar por dias corridos, não é preciso verificar dias não úteis
            if (CalculoDiarias.FlagCobrarDiariasDiasCorridos == "S")
            {
                return dataVencimento;
            }

            // Se for identificado que o dia é Sábado, Domingo ou Feriado, irá somar dias até que o dia seja um dia útil
            while (true)
            {
                if (dataVencimento.DayOfWeek == DayOfWeek.Saturday) //1. Se for Sábado
                {
                    dataVencimento = dataVencimento.AddDays(2);
                }
                else if (dataVencimento.DayOfWeek == DayOfWeek.Sunday) //2. Se for Domingo
                {
                    dataVencimento = dataVencimento.AddDays(1);
                }
                else if (CalculoDiarias.Feriados != null) //3. Se for Feriado
                {
                    if (CalculoDiarias.Feriados.Any(x => x.Date == dataVencimento.Date))
                    {
                        // É feriado
                        dataVencimento = dataVencimento.AddDays(1);
                    }
                    else
                    {
                        break; // Não é feriado
                    }
                }
            }

            return dataVencimento;
        }

        public MensagemViewModel UpdateFormaPagamento(int FaturamentoId, byte TipoMeioCobrancaId, int UsuarioId)
        {
            MensagemViewModel ResultView = new();

            List<string> erros = new();

            if (FaturamentoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);
            }

            if (TipoMeioCobrancaId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorFormaPagamentoInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.SetBadRequest(erros);
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                return MensagemViewHelper.SetUnauthorized();
            }

            FaturamentoModel Faturamento = _context.Faturamento
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.Grv)
                .ThenInclude(x => x.Cliente)
                .AsNoTracking()
                .FirstOrDefault(x => x.FaturamentoId == FaturamentoId);

            if (Faturamento == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);
            }
            else if (Faturamento.Status == "C")
            {
                return MensagemViewHelper.SetBadRequest("Esse Faturamento foi cancelado");
            }
            else if (Faturamento.Status == "P")
            {
                return MensagemViewHelper.SetBadRequest("Esse Faturamento já foi pago");
            }
            else if (Faturamento.TipoMeioCobrancaId == TipoMeioCobrancaId)
            {
                return MensagemViewHelper.SetBadRequest("Forma de Pagamento já selecionado");
            }

            TipoMeioCobrancaModel TipoMeioCobranca = _context.TipoMeioCobranca
                .AsNoTracking()
                .FirstOrDefault(x => x.TipoMeioCobrancaId == TipoMeioCobrancaId);

            if (TipoMeioCobranca == null)
            {
                return MensagemViewHelper.SetBadRequest($"Forma de Pagamento inexistente: {TipoMeioCobrancaId}");
            }
            else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixEstatico &&
                     Faturamento.Atendimento.Grv.Cliente.FlagPossuiPixEstatico == "N")
            {
                return MensagemViewHelper.SetBadRequest("Este Cliente não está configurado para emitir a Forma de Pagamento PIX Estático");
            }
            else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixDinamico &&
                     Faturamento.Atendimento.Grv.Cliente.FlagPossuiPixDinamico == "N")
            {
                return MensagemViewHelper.SetBadRequest("Este Cliente não está configurado para emitir a Forma de Pagamento PIX Dinâmico");
            }

            FaturamentoModel FaturamentoUpdate = _context.Faturamento
                .FirstOrDefault(x => x.FaturamentoId == FaturamentoId);

            FaturamentoUpdate.TipoMeioCobrancaId = TipoMeioCobrancaId;

            using IDbContextTransaction transaction = _context.Database.BeginTransaction();

            try
            {
                DeleteTipoMeioCobrancaAtual(FaturamentoId, Faturamento.TipoMeioCobranca);

                _context.Faturamento.Update(FaturamentoUpdate);

                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao alterar a Forma de Pagamento", ex);
            }

            return MensagemViewHelper.SetOk("Forma de Pagamento alterado com sucesso");
        }

        private void DeleteTipoMeioCobrancaAtual(int FaturamentoId, TipoMeioCobrancaModel TipoMeioCobrancaAtual)
        {
            if (TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.Boleto ||
                TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.BoletoEspecial)
            {
                new BoletoService(_context, _httpClientFactory)
                    .Cancel(FaturamentoId);
            }
            else if (TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.PixEstatico)
            {
                _context.PixEstatico
                    .Where(x => x.FaturamentoId == FaturamentoId)
                    .Delete();
            }
            else if (TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.PixDinamico)
            {
                _context.PixDinamico
                    .Where(x => x.FaturamentoId == FaturamentoId)
                    .Delete();
            }
        }

        public async Task<FaturamentoProdutoViewModelList> ListProdutosAsync()
        {
            FaturamentoProdutoViewModelList ResultView = new();

            List<FaturamentoProdutoModel> result = await _context.FaturamentoProduto
                .AsNoTracking()
                .ToListAsync();

            ResultView.Listagem = _mapper
                .Map<List<FaturamentoProdutoViewModel>>(result
                .OrderBy(x => x.Descricao)
                .ToList());

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }

        public async Task<ServicoAssociadoTipoVeiculoViewModelList> ListServicoAssociadoTipoVeiculoAsync(int GrvId, int UsuarioId)
        {
            ServicoAssociadoTipoVeiculoViewModelList ResultView = new();

            MensagemViewModel Mensagem = new GrvService(_context)
                .ValidateInputGrv(GrvId, UsuarioId);

            if (Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                ResultView.Mensagem = Mensagem;

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            List<ViewFaturamentoServicoAssociadoVeiculoModel> result = _context
                .ViewFaturamentoServicoAssociadoVeiculo
                .Where(x => x.ClienteId == Grv.ClienteId
                         && x.DepositoId == Grv.DepositoId
                         && x.TipoVeiculoId == Grv.TipoVeiculoId
                         && x.FaturamentoProdutoId == Grv.FaturamentoProdutoId
                         && (new[] { "DEP", "DRF" }.Contains(Grv.FaturamentoProdutoId) ? x.FlagCobrarGgv == "S" : true)
                         && x.DataVigenciaFinal == null)
                .AsNoTracking()
                .ToList();

            if (result?.Count > 0)
            {
                foreach (var item in result)
                {
                    if (item.FlagNaoCobrarSeNaoUsouReboque == "N" && Grv.FlagComboio == "S")
                    {
                        continue;
                    }
                    else if (item.FlagServicoObrigatorio == "S" || item.FlagServicoObrigatorioGlobal == "S")
                    {
                        continue;
                    }

                    ResultView.Listagem.Add(new()
                    {
                        IdentificadorServicoAssociadoTipoVeiculo = item.FaturamentoServicoTipoVeiculoId,

                        DescricaoServico = item.ServicoDescricao,

                        TipoCobranca = item.TipoCobranca,

                        DescricaoTipoCobranca = item.TipoCobrancaDescricao,

                        FlagPermiteAlteracaoValor = item.FlagPermiteAlteracaoValor,

                        PrecoPadrao = item.PrecoPadrao,

                        PrecoMinimoObrigatorio = item.PrecoValorMinimo,

                        DataVigenciaInicial = item.DataVigenciaInicial.Date
                    });
                }

                if (ResultView.Listagem.Count > 0)
                {
                    ResultView.Mensagem = MensagemViewHelper
                        .SetFound(ResultView.Listagem.Count);
                }
                else
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound();
                }
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ServicoAssociadoGrvViewModelList> ListServicoAssociadoGrvAsync(int GrvId, int UsuarioId)
        {
            ServicoAssociadoGrvViewModelList ResultView = new();

            MensagemViewModel Mensagem = new GrvService(_context)
                .ValidateInputGrv(GrvId, UsuarioId);

            if (Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                ResultView.Mensagem = Mensagem;

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            var result = _context.ViewFaturamentoServicoGrv
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .ToList();

            if (result?.Count > 0)
            {
                foreach (var item in result)
                {
                    if (item.FlagNaoCobrarSeNaoUsouReboque == "N" && Grv.FlagComboio == "S")
                    {
                        continue;
                    }
                    else if (item.FlagServicoObrigatorio == "S" || item.FlagServicoObrigatorioGlobal == "S")
                    {
                        continue;
                    }

                    //ResultView.Listagem.Add(new()
                    //{
                    //    ServicoAssociadoTipoVeiculoId = item.FaturamentoServicoTipoVeiculoId,

                    //    ServicoDescricao = item.ServicoDescricao,

                    //    TipoCobranca = item.TipoCobranca,

                    //    FlagPermiteAlteracaoValor = item.FlagPermiteAlteracaoValor,

                    //    PrecoPadrao = item.PrecoPadrao,

                    //    PrecoValorMinimo = item.PrecoValorMinimo,

                    //    DataVigenciaInicial = item.DataVigenciaInicial
                    //});
                }

                if (ResultView.Listagem.Count > 0)
                {
                    ResultView.Mensagem = MensagemViewHelper
                        .SetFound(ResultView.Listagem.Count);
                }
                else
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound();
                }
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<int> GetUltimoFaturamentoIdAsync(int GrvId)
        {
            GrvModel Grv = await _context.Grv
                .Include(x => x.Atendimento)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            if (Grv == null)
            {
                return 0;
            }

            int? FaturamentoId = await _context.Faturamento
                .Where(x => x.AtendimentoId == Grv.Atendimento.AtendimentoId && x.Status != "C")
                .AsNoTracking()
                .OrderByDescending(x => x.FaturamentoId)
                .Select(x => x.FaturamentoId)
                .FirstOrDefaultAsync();

            if (FaturamentoId == null)
            {
                return 0;
            }

            return FaturamentoId.Value;
        }
    }
}
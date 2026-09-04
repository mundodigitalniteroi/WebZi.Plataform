using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Banco.PIX;
using WebZi.Plataform.Data.Services.ClienteDeposito;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.DetranHub;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.Atendimento;
using WebZi.Plataform.Domain.DTO.Banco.PIX;
using WebZi.Plataform.Domain.DTO.Faturamento;
using WebZi.Plataform.Domain.DTO.Faturamento.Cadastro;
using WebZi.Plataform.Domain.DTO.Faturamento.Servico;
using WebZi.Plataform.Domain.DTO.Faturamento.Simulacao;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Liberacao;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.WebServices.DetranRio;
using WebZi.Plataform.Domain.DTO.WebServices.Nfe;
using WebZi.Plataform.Domain.DTO.WebServices.Nfse;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Liberacao;
using WebZi.Plataform.Domain.Models.Nfe;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Options;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Pagamento;
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
        private readonly IOptions<DetranHubOptions> _detranHubOptions;

        public FaturamentoService(AppDbContext context)
        {
            _context = context;
        }

        public FaturamentoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory,
            IOptions<DetranHubOptions> detranHubOptions = null)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _detranHubOptions = detranHubOptions;
        }

        private static FaturamentoComposicaoModel AplicarDesconto(FaturamentoComposicaoModel FaturamentoComposicao,
            List<CalculoFaturamentoDescontoModel> ListFaturamentoDesconto)
        {
            if (ListFaturamentoDesconto != null)
            {
                CalculoFaturamentoDescontoModel FaturamentoDesconto = ListFaturamentoDesconto
                    .Find(w => w.FaturamentoServicoTipoVeiculoId ==
                               FaturamentoComposicao.FaturamentoServicoTipoVeiculoId);

                if (FaturamentoDesconto != null)
                {
                    FaturamentoComposicao.UsuarioDescontoId = FaturamentoDesconto.UsuarioDescontoId;

                    FaturamentoComposicao.TipoDesconto = FaturamentoDesconto.TipoDesconto;

                    FaturamentoComposicao.ValorDesconto = FaturamentoDesconto.ValorDesconto;

                    FaturamentoComposicao.QuantidadeDesconto = FaturamentoDesconto.QuantidadeDesconto;

                    FaturamentoComposicao.ObservacaoDesconto = FaturamentoDesconto.ObservacaoDesconto;

                    if (FaturamentoComposicao.TipoDesconto == TipoDescontoFaturamentoEnum.Valor)
                    {
                        FaturamentoComposicao.ValorFaturado =
                            Math.Round(FaturamentoComposicao.ValorComposicao - FaturamentoDesconto.ValorDesconto, 2);
                    }
                    else
                    {
                        FaturamentoComposicao.ValorFaturado = Math.Round(
                            NumberHelper.GetPercentage(FaturamentoComposicao.ValorComposicao,
                                FaturamentoDesconto.QuantidadeDesconto), 2);
                    }
                }
            }

            return FaturamentoComposicao;
        }

        private static FaturamentoComposicaoModel AplicarQuantidadeAlterada(
            FaturamentoComposicaoModel FaturamentoComposicao,
            CalculoFaturamentoQuantidadeAlteradaModel FaturamentoQuantidadeAlterada, int quantidadeCalculada)
        {
            int quantidadeAjuste = FaturamentoQuantidadeAlterada.QuantidadeAjuste ?? 0;

            if (quantidadeAjuste < 0)
            {
                int quantidadeARemover = Math.Abs(quantidadeAjuste);
                if (quantidadeARemover > quantidadeCalculada)
                {
                    throw new ArgumentException(
                        $"A quantidade a remover ({quantidadeARemover}) não pode ser maior que a quantidade calculada ({quantidadeCalculada})");
                }
            }

            int quantidadeFinal = quantidadeCalculada + quantidadeAjuste;
            if (quantidadeFinal < 0)
            {
                throw new ArgumentException(
                    $"A quantidade final ({quantidadeFinal}) não pode ser menor que 0");
            }

            FaturamentoQuantidadeAlterada.QuantidadeAlterada = quantidadeAjuste;

            FaturamentoComposicao.UsuarioAlteracaoQuantidadeId =
                FaturamentoQuantidadeAlterada.UsuarioAlteracaoQuantidadeId;

            FaturamentoComposicao.QuantidadeAlterada = FaturamentoQuantidadeAlterada.QuantidadeAlterada;

            FaturamentoComposicao.ObservacaoQuantidadeAlterada =
                FaturamentoQuantidadeAlterada.ObservacaoQuantidadeAlterada;

            return FaturamentoComposicao;
        }

        private static DateTime CalcularDataVencimento(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento,
            CalculoDiariasModel CalculoDiarias, DateTime? dataFinal = null)
        {
            if (!dataFinal.HasValue)
            {
                dataFinal = ParametrosCalculoFaturamento.DataHoraPorDeposito;
            }

            DateTime dataVencimento = dataFinal.Value;

            dataVencimento = dataVencimento.SetTime(23, 59, 59);

            // Se for pra cobrar por dias corridos, não é preciso verificar dias não úteis
            if (CalculoDiarias.FlagCobrarDiariasDiasCorridos)
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

        private static List<CalculoTributacaoModel> CalcularTributacao(AppDbContext _context,
            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, decimal valorCalculado,
            string notaFiscalCnpj, string notaFiscalMunicipio, string notaFiscalUF)
        {
            if (valorCalculado <= 0 && string.IsNullOrWhiteSpace(notaFiscalCnpj) ||
                string.IsNullOrWhiteSpace(notaFiscalMunicipio) || string.IsNullOrWhiteSpace(notaFiscalUF))
            {
                return null;
            }

            List<ViewFaturamentoServicoAssociadoVeiculoModel> ServicosTributados = _context
                .ViewFaturamentoServicoAssociadoVeiculo
                .Where(x => x.ClienteId == ParametrosCalculoFaturamento.ClienteDeposito.ClienteId
                            && x.DepositoId == ParametrosCalculoFaturamento.ClienteDeposito.DepositoId
                            && x.TipoVeiculoId == ParametrosCalculoFaturamento.TipoVeiculoId
                            && x.FaturamentoProdutoId == ParametrosCalculoFaturamento.FaturamentoProdutoId
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
                .FirstOrDefault(x => x.CEPId == ParametrosCalculoFaturamento.ClienteDeposito.Deposito.CEPId);

            if (Endereco == null)
            {
                return null;
            }

            if (StringHelper.Normalize(notaFiscalMunicipio) != StringHelper.Normalize(Endereco.Municipio) ||
                notaFiscalUF != Endereco.UF)
            {
                return null;
            }

            #region Selecionar Regras do Faturamento

            FaturamentoRegraModel FaturamentoRegra = _context.FaturamentoRegra
                .Include(x => x.FaturamentoRegraTipo)
                .Where(x => x.ClienteId == ParametrosCalculoFaturamento.ClienteDeposito.ClienteId
                            && x.DepositoId == ParametrosCalculoFaturamento.ClienteDeposito.DepositoId
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

        private static bool CheckServicoDeveSerCalculado(ViewFaturamentoServicoGrvModel FaturamentoServicoGrv,
            FaturamentoModel UltimoFaturamento, CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (ParametrosCalculoFaturamento.FaturamentoProdutoId != "DEP" &&
                ParametrosCalculoFaturamento.FaturamentoProdutoId != "DRF" &&
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
            else if (ParametrosCalculoFaturamento.IsComboio &&
                     FaturamentoServicoGrv.FlagRebocada == "S")
            {
                // Não cobrar rebocada caso o veículo entrou no Depósito por Comboio
                return false;
            }
            else if (FaturamentoServicoGrv.FaturamentoRegraTipoCodigo != null &&
                     FaturamentoServicoGrv.FaturamentoRegraTipoCodigo ==
                     FaturamentoRegraTipoEnum.CobrarTarifaBancaria &&
                     !ParametrosCalculoFaturamento.TipoMeioCobrancaId.Equals(1))
            {
                // Se o serviço tiver a regra "Cobrança de Tarifa Bancária" e se o Tipo do Meio de Cobrança for Boleto
                return false;
            }

            return true;
        }

        private static string CreateNumeroIdentificacao(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento,
            int Sequencia)
        {
            return StringHelper.AddCharToLeft(ParametrosCalculoFaturamento.NumeroFormularioGrv, '0', 9) +
                   StringHelper.AddCharToLeft(ParametrosCalculoFaturamento.ClienteDeposito.DepositoId.ToString(), '0',
                       4) +
                   StringHelper.AddCharToLeft(Sequencia.ToString(), '0', 3);
        }

        private async Task DeleteTipoMeioCobrancaAtual(int FaturamentoId, TipoMeioCobrancaModel TipoMeioCobrancaAtual,
            CancellationToken ct)
        {
            if (TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.Boleto ||
                TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.BoletoEspecial)
            {
                new BoletoService(_context, _httpClientFactory)
                    .Cancel(FaturamentoId);
            }
            else if (TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.PixEstatico)
            {
                await _context.PixEstatico
                    .Where(x => x.FaturamentoId == FaturamentoId)
                    .DeleteAsync(ct);
            }
            else if (TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.PixDinamico)
            {
                int? pixDinamicoId = await _context.PixDinamico
                    .Where(x => x.FaturamentoId == FaturamentoId)
                    .Select(x => x.PixDinamicoId)
                    .FirstOrDefaultAsync(ct);

                if (pixDinamicoId.HasValue)
                {
                    await _context.PixDinamicoConsulta
                        .Where(x => x.PixDinamicoId == pixDinamicoId)
                        .DeleteAsync(ct);
                    await _context.PixDinamico
                        .Where(x => x.FaturamentoId == FaturamentoId)
                        .DeleteAsync(ct);
                }
            }
        }

        public FaturamentoModel Faturar(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento,
            out CalculoDiariasModel CalculoDiarias)
        {
            if (ParametrosCalculoFaturamento.DataHoraFinalParaCalculo == DateTime.MinValue)
            {
                ParametrosCalculoFaturamento.DataHoraFinalParaCalculo =
                    ParametrosCalculoFaturamento.DataHoraPorDeposito;
            }

            #region Selecionar o Atendimento

            AtendimentoModel Atendimento = new();

            if (!ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                Atendimento = _context.Atendimento
                    .AsNoTracking()
                    .FirstOrDefault(x => x.GrvId == ParametrosCalculoFaturamento.GrvId);
            }

            #endregion Selecionar o Atendimento

            #region Verificação de Faturamentos anteriores

            // Faturamento.Status:
            // N = Novo Faturamento/Não Pago;
            // A = Faturamento Adicional e Não Pago (Pra quando a Fatura foi paga em atraso);
            // C = Cancelado, pra quando foi gerada uma Fatura para uma Fatura Vencida e que não foi paga;
            // P = Fatura Paga.

            // Se existir ao menos 1 Fatura paga, não deve dar Desconto
            if (Atendimento != null && !ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                if (_context.Faturamento
                    .Where(x => x.AtendimentoId == Atendimento.AtendimentoId
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

            if (Atendimento != null && !ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                UltimoFaturamento = _context.Faturamento
                    .OrderByDescending(x => x.FaturamentoId)
                    .FirstOrDefault(x => x.AtendimentoId == Atendimento.AtendimentoId);

                if (UltimoFaturamento != null)
                {
                    #region Cancelar o Faturamento atual

                    string statusAnterior = UltimoFaturamento.Status;

                    if (UltimoFaturamento.Status != "P")
                    {
                        UltimoFaturamento.UsuarioAlteracaoId = Atendimento.UsuarioCadastroId;

                        UltimoFaturamento.Status = "C";

                        UltimoFaturamento.DataAlteracao = ParametrosCalculoFaturamento.DataHoraPorDeposito;

                        if (!ParametrosCalculoFaturamento.IsSimulacao)
                        {
                            _context.Faturamento.Update(UltimoFaturamento);
                        }
                    }

                    if (ParametrosCalculoFaturamento.TipoMeioCobrancaId == 0)
                    {
                        ParametrosCalculoFaturamento.TipoMeioCobrancaId = UltimoFaturamento.TipoMeioCobrancaId;
                    }

                    #endregion Cancelar o Faturamento atual

                    // Se a Fatura for Nova, então está sendo cancelada e gerada uma nova, incluindo a aplicação dos Descontos caso houver.
                    if (statusAnterior == "A" || ParametrosCalculoFaturamento.FaturamentoAdicional)
                    {
                        ParametrosCalculoFaturamento.FlagFaturamentoCompleto = false;
                    }
                }
            }

            #endregion Verificação de Faturamentos anteriores


            #region Selecionar os Serviços cadastrados no GRV

            List<ViewFaturamentoServicoGrvModel> FaturamentoServicosGrvs = new();

            if (!ParametrosCalculoFaturamento.FaturarSemGrv &&
                (!ParametrosCalculoFaturamento.IsLeilaoStatus || UltimoFaturamento != null))
            {
                FaturamentoServicosGrvs = _context.ViewFaturamentoServicoGrv
                    .Where(x => x.GrvId == ParametrosCalculoFaturamento.GrvId &&
                                x.FaturamentoProdutoId == ParametrosCalculoFaturamento.FaturamentoProdutoId &&
                                x.FlagTributacao == "N" &&
                                (x.FlagRealizarCobranca == null || x.FlagRealizarCobranca == "S") &&
                                (x.FlagCobrarGGV == "N" || (x.FlagCobrarGGV == "S" && x.Valor > 0)))
                    .AsNoTracking()
                    .ToList();

                if (FaturamentoServicosGrvs?.Count == 0)
                {
                    throw new Exception("Não foi encontrado Serviço associado à este Processo");
                }
            }

            #endregion Selecionar os Serviços cadastrados no GRV

            #region Selecionar todos os Serviços associados ao CLIDEP, incluindo os com a Vigência finalizada

            List<ViewFaturamentoServicoAssociadoVeiculoModel> FaturamentoServicosAssociadosVeiculos = _context
                .ViewFaturamentoServicoAssociadoVeiculo
                .Where(x => x.ClienteId == ParametrosCalculoFaturamento.ClienteDeposito.ClienteId &&
                            x.DepositoId == ParametrosCalculoFaturamento.ClienteDeposito.DepositoId &&
                            x.TipoVeiculoId == ParametrosCalculoFaturamento.TipoVeiculoId &&
                            x.FaturamentoProdutoId == ParametrosCalculoFaturamento.FaturamentoProdutoId &&
                            x.DataVigenciaFinal == null)
                .AsNoTracking()
                .ToList();

            if (FaturamentoServicosAssociadosVeiculos?.Count == 0)
            {
                throw new Exception(
                    "Não foi encontrado Serviço associado ao Cliente + Depósito + Tipo de Veículo + Produto");
            }

            if (ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                FaturamentoServicosGrvs = _mapper
                    .Map<List<ViewFaturamentoServicoGrvModel>>(FaturamentoServicosAssociadosVeiculos
                        .Where(x => x.DataVigenciaFinal == null)
                        .ToList());
            }

            #endregion Selecionar todos os Serviços associados ao CLIDEP, incluindo os com a Vigência finalizada


            #region Cálculo das Diárias

            CalculoDiarias = new CalculoDiariasService(_context)
                .Calcular(ParametrosCalculoFaturamento);

            ParametrosCalculoFaturamento.DataHoraInicialParaCalculo = CalculoDiarias.DataHoraInicialParaCalculo;

            #endregion Cálculo das Diárias

            #region Composição do Faturamento

            CalculoFaturamentoQuantidadeAlteradaModel FaturamentoQuantidadeAlterada = new();

            List<FaturamentoComposicaoModel> FaturamentoComposicoes = new();

            FaturamentoComposicaoModel FaturamentoComposicao = new();

            ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculo = new();

            List<ViewFaturamentoServicoAssociadoVeiculoModel>
                FaturamentoServicosAssociadosVeiculosTodasVigenciasEncontradas = new();

            decimal ValorFaturado = 0;
            int DiariasCalculadas = 0;
            int Horas = 0;

            foreach (ViewFaturamentoServicoGrvModel FaturamentoServicoGrv in FaturamentoServicosGrvs)
            {
                if (!CheckServicoDeveSerCalculado(FaturamentoServicoGrv, UltimoFaturamento,
                        ParametrosCalculoFaturamento))
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

                    if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto &&
                        ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas?.Count > 0)
                    {
                        FaturamentoQuantidadeAlterada = ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas
                            .Find(w => w.FaturamentoServicoTipoVeiculoId ==
                                       FaturamentoServicoGrv.FaturamentoServicoTipoVeiculoId);
                    }

                    if (new[] { "AM", "VI" }.Contains(FaturamentoServicoGrv.FormaCobranca))
                    {
                        DiariasCalculadas = CalculoDiarias.Diarias;

                        if (FaturamentoServicoGrv.FormaCobranca == "AM")
                        {
                            // Primeiro filtro, cobrar por todas as vigências encontradas
                            FaturamentoServicosAssociadosVeiculosTodasVigenciasEncontradas =
                                FaturamentoServicosAssociadosVeiculos
                                    .Where(x => (x.FaturamentoServicoTipoId ==
                                                 FaturamentoServicoGrv.FaturamentoServicoTipoId &&
                                                 (ParametrosCalculoFaturamento.DataHoraInicialParaCalculo.Date >=
                                                  x.DataVigenciaInicial &&
                                                  ParametrosCalculoFaturamento.DataHoraInicialParaCalculo.Date <=
                                                  x.DataVigenciaFinal)) ||
                                                ParametrosCalculoFaturamento.DataHoraInicialParaCalculo <=
                                                x.DataVigenciaFinal || x.DataVigenciaFinal == null)
                                    .ToList();

                            foreach (ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculoAmbos
                                     in FaturamentoServicosAssociadosVeiculosTodasVigenciasEncontradas)
                            {
                                // Retorna a quantidade de Dias entre as datas
                                CalculoDiarias.Diarias = GetQuantidadeDiasServicoDiarias(
                                    FaturamentoServicoAssociadoVeiculoAmbos,
                                    ParametrosCalculoFaturamento.DataHoraInicialParaCalculo,
                                    ParametrosCalculoFaturamento.DataHoraPorDeposito);

                                if (CalculoDiarias.Diarias >= DiariasCalculadas)
                                {
                                    CalculoDiarias.Diarias = DiariasCalculadas;

                                    DiariasCalculadas = 0;
                                }
                                else
                                {
                                    DiariasCalculadas -= CalculoDiarias.Diarias;
                                }

                                FaturamentoComposicao.FaturamentoServicoTipoVeiculoId =
                                    FaturamentoServicoAssociadoVeiculoAmbos.FaturamentoServicoTipoVeiculoId;

                                FaturamentoComposicao.TipoComposicao =
                                    FaturamentoServicoAssociadoVeiculoAmbos.TipoCobranca;

                                FaturamentoComposicao.ValorTipoComposicao =
                                    FaturamentoServicoAssociadoVeiculoAmbos.PrecoPadrao;

                                // Aplicar Quantidade Alterada
                                if (FaturamentoQuantidadeAlterada != null)
                                {
                                    int quantidadeCalculada = CalculoDiarias.Diarias;
                                    int quantidadeAjuste = FaturamentoQuantidadeAlterada.QuantidadeAjuste ?? 0;
                                    CalculoDiarias.Diarias = quantidadeCalculada + quantidadeAjuste;
                                    if (CalculoDiarias.Diarias <= 0)
                                    {
                                        throw new ArgumentException(
                                            $"A quantidade final não pode ser zero ou negativa. Quantidade calculada: {quantidadeCalculada}, Ajuste: {quantidadeAjuste}");
                                    }

                                    FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao,
                                        FaturamentoQuantidadeAlterada, quantidadeCalculada);
                                }

                                FaturamentoComposicao.QuantidadeComposicao = CalculoDiarias.Diarias;

                                FaturamentoComposicao.ValorComposicao = Math.Round(
                                    FaturamentoServicoAssociadoVeiculoAmbos.PrecoPadrao * CalculoDiarias.Diarias, 2);

                                FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;

                                // Aplicar os Descontos
                                if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto &&
                                    ParametrosCalculoFaturamento.FaturamentoDescontos?.Count > 0)
                                {
                                    FaturamentoComposicao = AplicarDesconto(FaturamentoComposicao,
                                        ParametrosCalculoFaturamento.FaturamentoDescontos);
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
                                .FirstOrDefault(x =>
                                    x.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId
                                    && (x.DataVigenciaFinal >=
                                        ParametrosCalculoFaturamento.DataHoraInicialParaCalculo.Date ||
                                        x.DataVigenciaFinal == null));

                            // Aplicar Quantidade Alterada
                            if (FaturamentoQuantidadeAlterada != null)
                            {
                                int quantidadeCalculada = CalculoDiarias.Diarias;

                                FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao,
                                    FaturamentoQuantidadeAlterada, quantidadeCalculada);

                                CalculoDiarias.Diarias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                            }

                            FaturamentoComposicao.TipoComposicao = FaturamentoServicoAssociadoVeiculo.TipoCobranca;

                            FaturamentoComposicao.FaturamentoServicoTipoVeiculoId = FaturamentoServicoAssociadoVeiculo
                                .FaturamentoServicoTipoVeiculoId;

                            FaturamentoComposicao.ValorTipoComposicao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;

                            FaturamentoComposicao.QuantidadeComposicao = CalculoDiarias.Diarias;

                            FaturamentoComposicao.ValorComposicao =
                                Math.Round(FaturamentoComposicao.ValorTipoComposicao * CalculoDiarias.Diarias, 2);

                            FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                        }
                    }
                    else
                    {
                        // Aplicar Quantidade Alterada
                        if (FaturamentoQuantidadeAlterada != null)
                        {
                            int quantidadeCalculada = CalculoDiarias.Diarias;

                            FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao,
                                FaturamentoQuantidadeAlterada, quantidadeCalculada);

                            CalculoDiarias.Diarias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                        }

                        FaturamentoComposicao.QuantidadeComposicao = CalculoDiarias.Diarias;

                        FaturamentoComposicao.ValorComposicao =
                            Math.Round(FaturamentoServicoGrv.PrecoPadrao * CalculoDiarias.Diarias, 2);

                        FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                    }
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Horas)
                {
                    if (string.IsNullOrWhiteSpace(FaturamentoServicoGrv.TempoTrabalhado))
                    {
                        continue;
                    }

                    FaturamentoComposicao.QuantidadeComposicao = Math.Round(
                        Convert.ToDecimal(TimeSpan.Parse(FaturamentoServicoGrv.TempoTrabalhado).TotalHours), 2);

                    decimal precoUnitario =
                        (FaturamentoServicoGrv.Valor.HasValue && FaturamentoServicoGrv.Valor.Value > 0)
                            ? FaturamentoServicoGrv.Valor.Value
                            : FaturamentoServicoGrv.PrecoPadrao;

                    FaturamentoComposicao.ValorTipoComposicao = precoUnitario;

                    FaturamentoComposicao.ValorComposicao = Math.Round(
                        precoUnitario * FaturamentoComposicao.QuantidadeComposicao.Value, 2);

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
                                .FirstOrDefault(x =>
                                    x.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId
                                    && (x.DataVigenciaFinal >=
                                        ParametrosCalculoFaturamento.DataHoraInicialParaCalculo.Date ||
                                        x.DataVigenciaFinal == null));

                            FaturamentoServicoGrv.PrecoPadrao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;

                            FaturamentoComposicao.FaturamentoServicoTipoVeiculoId = FaturamentoServicoAssociadoVeiculo
                                .FaturamentoServicoTipoVeiculoId;

                            FaturamentoComposicao.ValorTipoComposicao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;
                        }
                    }
                    else
                    {
                        FaturamentoComposicao.QuantidadeComposicao =
                            FaturamentoServicoGrv.QuantidadeDesconto.HasValue &&
                            FaturamentoServicoGrv.QuantidadeDesconto.Value > 0
                                ? FaturamentoServicoGrv.QuantidadeDesconto.Value
                                : (FaturamentoServicoGrv.Valor.HasValue
                                    ? Math.Round(FaturamentoServicoGrv.Valor.Value, 2)
                                    : 1);
                    }

                    if (FaturamentoComposicao.QuantidadeComposicao == 0)
                    {
                        continue;
                    }

                    FaturamentoComposicao.ValorComposicao = Math.Round(
                        FaturamentoServicoGrv.PrecoPadrao *
                        Math.Round(FaturamentoComposicao.QuantidadeComposicao.Value, 2), 2);

                    FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Valor)
                {
                    if (FaturamentoServicoGrv.FlagPermiteAlteracaoValor == "N" &&
                        (FaturamentoServicoGrv.PrecoPadrao > 0) && (FaturamentoServicoGrv.Valor == 0))
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

                        FaturamentoComposicao.ValorComposicao =
                            Math.Round(FaturamentoServicoGrv.PrecoPadrao * FaturamentoServicoGrv.Valor.Value, 2);

                        FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                    }
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Tempo)
                {
                    if ((Horas = (int)(ParametrosCalculoFaturamento.DataHoraPorDeposito -
                                       ParametrosCalculoFaturamento.DataHoraInicialParaCalculo).TotalHours) == 0)
                    {
                        Horas++;
                    }

                    FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * Horas, 2);

                    FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                }

                // Aplicar os Descontos
                if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto &&
                    ParametrosCalculoFaturamento.FaturamentoDescontos?.Count > 0)
                {
                    FaturamentoComposicao = AplicarDesconto(FaturamentoComposicao,
                        ParametrosCalculoFaturamento.FaturamentoDescontos);
                }

                FaturamentoComposicao.TipoLancamento = TipoLancamentoFaturamentoEnum.Crédito;

                ValorFaturado += FaturamentoComposicao.ValorFaturado;

                FaturamentoComposicoes.Add(FaturamentoComposicao);
            }

            #endregion Composição do Faturamento

            #region Tributação

            if (Atendimento != null && !ParametrosCalculoFaturamento.FaturarSemGrv && ValorFaturado > 0)
            {
                List<CalculoTributacaoModel> Tributacoes = CalcularTributacao(_context,
                    ParametrosCalculoFaturamento,
                    ValorFaturado,
                    Atendimento.NotaFiscalDocumento,
                    Atendimento.NotaFiscalMunicipio,
                    Atendimento.NotaFiscalUF);

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

            FaturamentoModel Faturamento = new();

            if (!ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                Faturamento = new()
                {
                    AtendimentoId = Atendimento != null ? Atendimento.AtendimentoId : 0,

                    UsuarioCadastroId = ParametrosCalculoFaturamento.UsuarioCadastroId,

                    TipoMeioCobrancaId = ParametrosCalculoFaturamento.TipoMeioCobrancaId,

                    HoraDiaria = CalculoDiarias.HoraDiaria,

                    MaximoDiariasParaCobranca = CalculoDiarias.MaximoDiariasParaCobranca,

                    MaximoDiasVencimento = CalculoDiarias.MaximoDiasVencimento,

                    FlagUsarHoraDiaria = CalculoDiarias.FlagUsarHoraDiaria ? "S" : "N",

                    FlagClienteRealizaFaturamentoArrecadacao =
                        CalculoDiarias.FlagClienteRealizaFaturamentoArrecadacao ? "S" : "N",

                    FlagCobrarDiariasDiasCorridos = CalculoDiarias.FlagCobrarDiariasDiasCorridos ? "S" : "N",

                    DataCalculo = CalculoDiarias.DataHoraInicialParaCalculo,

                    DataVencimento = CalcularDataVencimento(ParametrosCalculoFaturamento, CalculoDiarias,
                        ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento
                            ? ParametrosCalculoFaturamento.DataHoraPorDeposito
                            : ParametrosCalculoFaturamento.DataHoraFinalParaCalculo),

                    DataCadastro = ParametrosCalculoFaturamento.DataHoraPorDeposito,

                    ValorFaturado = ValorFaturado,

                    ListagemFaturamentoComposicao = FaturamentoComposicoes
                };
            }
            else
            {
                Faturamento = new()
                {
                    HoraDiaria = CalculoDiarias.HoraDiaria,

                    MaximoDiariasParaCobranca = CalculoDiarias.MaximoDiariasParaCobranca,

                    MaximoDiasVencimento = CalculoDiarias.MaximoDiasVencimento,

                    FlagUsarHoraDiaria = CalculoDiarias.FlagUsarHoraDiaria ? "S" : "N",

                    FlagClienteRealizaFaturamentoArrecadacao =
                        CalculoDiarias.FlagClienteRealizaFaturamentoArrecadacao ? "S" : "N",

                    FlagCobrarDiariasDiasCorridos = CalculoDiarias.FlagCobrarDiariasDiasCorridos ? "S" : "N",

                    DataCalculo = CalculoDiarias.DataHoraInicialParaCalculo,

                    DataVencimento = CalcularDataVencimento(ParametrosCalculoFaturamento, CalculoDiarias,
                        ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento
                            ? ParametrosCalculoFaturamento.DataHoraPorDeposito
                            : ParametrosCalculoFaturamento.DataHoraFinalParaCalculo),

                    DataCadastro = ParametrosCalculoFaturamento.DataHoraPorDeposito,

                    ValorFaturado = ValorFaturado,

                    ListagemFaturamentoComposicao = FaturamentoComposicoes
                };
            }

            if (ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento)
            {
                Faturamento.DataRetroativa = ParametrosCalculoFaturamento.DataHoraFinalParaCalculo.Date;

                Faturamento.FlagPermissaoDataRetroativaFaturamento = "S";
            }

            if (UltimoFaturamento != null)
            {
                Faturamento.Sequencia += UltimoFaturamento.Sequencia;
            }

            if (!ParametrosCalculoFaturamento.FaturarSemGrv && !ParametrosCalculoFaturamento.IsSimulacao)
            {
                Faturamento.NumeroIdentificacao =
                    CreateNumeroIdentificacao(ParametrosCalculoFaturamento, Faturamento.Sequencia);

                // _context.SetUserContextInfo(Faturamento.UsuarioCadastroId);

                _context.Faturamento.Add(Faturamento);
            }

            return Faturamento;

            #endregion Cadastro do Faturamento
        }

        private static int GetQuantidadeDiasServicoDiarias(
            ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculo, DateTime DataHoraGuarda,
            DateTime DataHoraAtualPorDeposito)
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
                DataFinal = new DateTime(FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Year,
                    FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Month,
                    FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Day, 23, 59, 59);
            }

            return 1 + DateTimeHelper.GetDaysBetweenTwoDates(DataInicial, DataFinal);
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

        public async Task<FaturamentoProdutoDTO> GetProdutoAsync(string FaturamentoProdutoId)
        {
            FaturamentoProdutoModel result = await _context.FaturamentoProduto
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FaturamentoProdutoId == FaturamentoProdutoId);

            if (result != null)
            {
                return _mapper
                    .Map<FaturamentoProdutoDTO>(result);
            }
            else
            {
                return null;
            }
        }

        public async Task<FaturamentoProdutoListDTO> ListProdutosAsync()
        {
            FaturamentoProdutoListDTO ResultView = new();

            List<FaturamentoProdutoModel> result = await _context.FaturamentoProduto
                .AsNoTracking()
                .ToListAsync();

            ResultView.Listagem = _mapper
                .Map<List<FaturamentoProdutoDTO>>(result
                    .OrderBy(x => x.Descricao)
                    .ToList());

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }

        public async Task<FaturamentoListDTO> ListByAtendimentoIdAsync(int AtendimentoId, int UsuarioId,
            bool SelecionarFaturasCanceladas)
        {
            List<string> erros = new();

            if (AtendimentoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorAtendimentoInvalido);
            }

            FaturamentoListDTO ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            AtendimentoModel Atendimento = await _context.Atendimento
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AtendimentoId == AtendimentoId);

            if (Atendimento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Este Processo não possui Atendimento");

                return ResultView;
            }

            return await ListByGrvIdAsync(Atendimento.GrvId, UsuarioId, SelecionarFaturasCanceladas);
        }

        public async Task<FaturamentoListDTO> ListByGrvIdAsync(int GrvId, int UsuarioId,
            bool SelecionarFaturasCanceladas)
        {
            FaturamentoListDTO ResultView = new()
            {
                Mensagem = new GrvService(_context)
                    .ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.ListagemFaturamento)
                .ThenInclude(x => x.ListagemFaturamentoComposicao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }
            else if (Grv.Atendimento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Este Processo não possui Atendimento");

                return ResultView;
            }
            else if (Grv.Atendimento.ListagemFaturamento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Este Processo não possui Faturamento");

                return ResultView;
            }

            List<TabelaGenericaModel> ListagemTipoCobranca = await new TabelaGenericaService(_context)
                .ListAsync("FAT_TIPO_COBRANCA");

            ResultView.IdentificadorProcesso = Grv.GrvId;

            ResultView.IdentificadorAtendimento = Grv.Atendimento.AtendimentoId;

            if (SelecionarFaturasCanceladas)
            {
                ResultView.ListagemFaturamento = _mapper
                    .Map<List<FaturamentoCadastroDTO>>(Grv.Atendimento.ListagemFaturamento
                        .OrderBy(x => x.DataCadastro));
            }
            else
            {
                ResultView.ListagemFaturamento = _mapper
                    .Map<List<FaturamentoCadastroDTO>>(Grv.Atendimento.ListagemFaturamento
                        .Where(x => x.Status != "C")
                        .OrderBy(x => x.DataCadastro));
            }

            FaturamentoServicoTipoVeiculoModel FaturamentoServicoTipoVeiculo = new();

            foreach (var Faturamento in ResultView.ListagemFaturamento)
            {
                foreach (var Servico in Faturamento.ListagemServico)
                {
                    FaturamentoServicoTipoVeiculo = _context.FaturamentoServicoTipoVeiculo
                        .Include(x => x.FaturamentoServicoAssociado)
                        .AsNoTracking()
                        .FirstOrDefault(x =>
                            x.FaturamentoServicoTipoVeiculoId == Servico.IdentificadorFaturamentoServicoTipoVeiculo);

                    Servico.DescricaoTipoServico = ListagemTipoCobranca
                        .Where(x => x.ValorCadastro == Servico.TipoServico).FirstOrDefault().Descricao;

                    Servico.NomeServico = FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.Descricao;

                    Servico.DataVigenciaInicial =
                        FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.DataVigenciaInicial;

                    Servico.DataVigenciaFinal =
                        FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.DataVigenciaFinal;
                }
            }

            ResultView.Mensagem = MensagemViewHelper.SetFound(Grv.Atendimento.ListagemFaturamento.Count);

            return ResultView;
        }

        public async Task<ServicoAssociadoTipoVeiculoListDTO> ListServicoAssociadoTipoVeiculoAsync(int GrvId,
            int UsuarioId, CancellationToken ct)
        {
            ServicoAssociadoTipoVeiculoListDTO ResultView = new();

            MensagemDTO Mensagem = new GrvService(_context)
                .ValidateInputGrv(GrvId, UsuarioId);

            if (Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                ResultView.Mensagem = Mensagem;

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId, cancellationToken: ct);

            List<ViewFaturamentoServicoAssociadoVeiculoModel> result = _context
                .ViewFaturamentoServicoAssociadoVeiculo
                .Where(x => x.ClienteId == Grv.ClienteId
                            && x.DepositoId == Grv.DepositoId
                            && x.TipoVeiculoId == Grv.TipoVeiculoId
                            && x.FaturamentoProdutoId == Grv.FaturamentoProdutoId
                            && (!new[] { "DEP", "DRF" }.Contains(Grv.FaturamentoProdutoId) || x.FlagCobrarGGV == "S")
                            && x.DataVigenciaFinal == null)
                .AsNoTracking()
                .ToList();

            if (result?.Count > 0)
            {
                foreach (ViewFaturamentoServicoAssociadoVeiculoModel item in result)
                {
                    if (item.FlagNaoCobrarSeNaoUsouReboque == "S" && Grv.FlagComboio == "S")
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

        public async Task<ServicoAssociadoGrvListDTO> ListServicoAssociadoGrvAsync(int GrvId, int UsuarioId,
            CancellationToken ct)
        {
            ServicoAssociadoGrvListDTO ResultView = new();

            MensagemDTO Mensagem = new GrvService(_context)
                .ValidateInputGrv(GrvId, UsuarioId);

            if (Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                ResultView.Mensagem = Mensagem;

                return ResultView;
            }

            // GrvModel Grv = await _context.Grv
            //     .AsNoTracking()
            //     .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            List<FaturamentoServicoGrvModel> result = await _context.FaturamentoServicoGrv
                .Include(x => x.FaturamentoServicoTipoVeiculo)
                .ThenInclude(x => x.FaturamentoServicoAssociado)
                .ThenInclude(x => x.FaturamentoServicoTipo)
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .ToListAsync(cancellationToken: ct);

            if (result.Any())
            {
                foreach (var item in result)
                {
                    decimal valor = item.Valor / (item.QuantidadeDesconto ?? 1);

                    ResultView.Listagem.Add(new()
                    {
                        IdentificadorServicoGrv = item.FaturamentoServicoGrvId,
                        identificadorServicoAssociadoTipoVeiculo = item.FaturamentoServicoTipoVeiculoId,
                        NomeServico = item.FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.Descricao,
                        GrvId = item.GrvId,
                        Valor = valor,
                        ValorTotal = valor * (item.QuantidadeDesconto ?? 1),
                        Quantidade = item.QuantidadeDesconto ?? 1,
                        TempoTrabalhado = item.TempoTrabalhado,
                        FlagRealizarCobranca = item.FlagRealizarCobranca,
                        TipoCobranca = item.FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado
                            .FaturamentoServicoTipo.TipoCobranca
                    });
                }
            }
            // if (result?.Count > 0)
            // {
            //     foreach (FaturamentoServicoGrvModel item in result)
            //     {
            //         ResultView.Listagem.Add(new()
            //         {
            //             IdentificadorServicoGrv = item.FaturamentoServicoGrvId,
            //             identificadorServicoAssociadoTipoVeiculo = item.FaturamentoServicoTipoVeiculoId,
            //             GrvId = item.GrvId,
            //             Valor = item.Valor / (item.QuantidadeDesconto ?? 1),
            //             ValorTotal = item.Valor * (item.QuantidadeDesconto ?? 1),
            //             Quantidade = item.QuantidadeDesconto ?? 1,
            //             FlagRealizarCobranca = item.FlagRealizarCobranca,
            //         });
            //     }

            if (ResultView.Listagem.Count < 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
                return ResultView;
            }

            ResultView.Mensagem = MensagemViewHelper
                .SetFound(ResultView.Listagem.Count);
            return ResultView;
        }

        public async Task<SimulacaoDTO> SimularAsync(SimulacaoParameters model, CancellationToken ct)
        {
            #region Consulta

            GrvModel Grv;

            if (model.IdentificadorProcesso > 0)
            {
                Grv = await _context.Grv
                    .Include(x => x.FaturamentoProduto)
                    .Include(x => x.TipoVeiculo)
                    .Include(x => x.Atendimento)
                    .Include(x => x.StatusOperacao)
                    .AsNoTracking()
                    .OrderByDescending(x => x.DataHoraRemocao)
                    .FirstOrDefaultAsync(x => x.GrvId == model.IdentificadorProcesso, cancellationToken: ct);
            }
            else
            {
                Grv = await _context.Grv
                    .Include(x => x.FaturamentoProduto)
                    .Include(x => x.TipoVeiculo)
                    .Include(x => x.Atendimento)
                    .Include(x => x.StatusOperacao)
                    .AsNoTracking()
                    .OrderByDescending(x => x.DataHoraRemocao)
                    .FirstOrDefaultAsync(x =>
                        !model.Placa.IsNullOrWhiteSpace()
                            ? x.Placa == model.Placa
                            : model.Chassi.IsNullOrWhiteSpace() || x.Chassi == model.Chassi, cancellationToken: ct);
            }

            #endregion

            SimulacaoDTO ResultView = new();

            #region Validações do Processo GRV

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNewMessage(ResultView.Mensagem,
                    MensagemPadraoEnum.NaoEncontradoGrv, MensagemTipoAvisoEnum.Impeditivo);

                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.NotFound;

                return ResultView;
            }

            ResultView.Mensagem = new GrvService(_context)
                .ValidateInputGrv(Grv.GrvId, model.IdentificadorUsuario);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (!new[] { "V", "1", "3", "7" }.Contains(Grv.StatusOperacaoId))
            {
                string descricaoStatus = Grv.StatusOperacao?.Descricao?.ToUpper();
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite a execução da Simulação. " +
                    $"Descrição do Status atual: {descricaoStatus}");

                return ResultView;
            }

            #endregion Validações do Processo GRV

            #region Validações e Ajustes dos Parâmetros

            List<string> erros = new();

            if (model.IdentificadorCliente is <= 0 or null)
            {
                model.IdentificadorCliente = Grv.ClienteId;
            }

            if (model.IdentificadorDeposito is <= 0 or null)
            {
                model.IdentificadorDeposito = Grv.DepositoId;
            }

            var podeEmitirNota = await _context.FaturamentoRegra
                .AnyAsync(x =>
                    x.ClienteId == model.IdentificadorCliente && x.DepositoId == model.IdentificadorDeposito &&
                    x.FaturamentoRegraTipoId == 11, cancellationToken: ct);

            if (model.IdentificadorProcesso <= 0 && model.Placa.IsNullOrWhiteSpace() &&
                model.Chassi.IsNullOrWhiteSpace())
            {
                erros.Add("Informe o Identificador do Processo, Placa ou Chassi");
            }

            if (model.IdentificadorProcesso is <= 0 or null)
            {
                if (!model.Placa.IsNullOrWhiteSpace() && !model.Placa.IsPlaca())
                {
                    erros.Add("Placa inválida");
                }
                else if (model.Placa.IsNullOrWhiteSpace() && (model.Chassi.Length < 6 || model.Chassi.Length > 24 ||
                                                              (model.Chassi.Length == 17 && !model.Chassi.IsChassi())))
                {
                    erros.Add("Chassi inválido");
                }
            }

            if (model.IdentificadorCliente is <= 0 or null)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (model.IdentificadorDeposito is <= 0 or null)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            DateTime DataHoraPorDeposito = DateTime.MinValue;

            if (model.IdentificadorDeposito > 0)
            {
                DataHoraPorDeposito = new DepositoService(_context)
                    .GetDataHoraPorDeposito(model.IdentificadorDeposito.Value);
            }

            if (model.DataHoraInicialParaCalculo == DateTime.MinValue || model.DataHoraInicialParaCalculo == default)
            {
                model.DataHoraInicialParaCalculo = Grv.DataHoraGuarda ?? Grv.DataHoraRemocao;
            }

            if (model.DataHoraFinalParaCalculo == DateTime.MinValue || model.DataHoraFinalParaCalculo == default)
            {
                model.DataHoraFinalParaCalculo =
                    DataHoraPorDeposito != DateTime.MinValue ? DataHoraPorDeposito : DateTime.Now;
            }

            var now = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)).DateTime;

            if (model.DataHoraFinalParaCalculo > now)
            {
                erros.Add("A Data/Hora Inicial para o Cálculo não pode ser maior do que a Data/Hora atual");
            }

            if (DataHoraPorDeposito != DateTime.MinValue && model.DataHoraInicialParaCalculo > DataHoraPorDeposito)
            {
                erros.Add("A Data/Hora Inicial para o Cálculo não pode ser maior do que a Data/Hora do Depósito");
            }

            if (model.DataHoraInicialParaCalculo > model.DataHoraFinalParaCalculo)
            {
                erros.Add(
                    "A Data/Hora Final para o Cálculo não pode ser menor do que a Data/Hora Inicial para o Cálculo");
            }

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            #endregion Validações e Ajustes dos Parâmetros

            #region Validações Adicionais

            ResultView.Produto = _mapper.Map<SimulacaoProdutoDTO>(Grv.FaturamentoProduto);

            ResultView.Mensagem = await new ClienteDepositoService(_context)
                .ValidateClienteDepositoAsync(model.IdentificadorCliente.Value, model.IdentificadorDeposito.Value);
            if (Grv.FlagComboio == "S")
                model.IsComboio = true;
            if (!Grv.Placa.IsNullOrWhiteSpace() || !Grv.Chassi.IsNullOrWhiteSpace())
            {
                var detranHubService = _detranHubOptions != null
                    ? new DetranHubService(_httpClientFactory, _mapper, _detranHubOptions)
                    : new DetranHubService(_httpClientFactory, _mapper);

                string placa = Grv.Placa.IsPlaca() ? Grv.Placa : null;
                string chassi = placa == null ? Grv.Chassi : null;

                var detranHubResult = await detranHubService.SearchToPlateOrChassi(placa, chassi);

                if (detranHubResult?.Veiculo != null)
                {
                    ResultView.Veiculo = _mapper.Map<DetranRioVeiculoDTO>(detranHubResult.Veiculo);
                    ResultView.Veiculo.Mensagem = detranHubResult.Mensagem;
                }
                else
                {
                    ResultView.Veiculo = new DetranRioVeiculoDTO
                    {
                        Mensagem = detranHubResult?.Mensagem ?? MensagemViewHelper.SetNotFound("Veículo não encontrado")
                    };
                }

                if (ResultView.Veiculo.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    // ResultView.Mensagem = MensagemViewHelper.SetNewMessages(ResultView.Mensagem, ResultView.Veiculo.Mensagem);
                }
                else if (ResultView.Veiculo.TipoVeiculo == null)
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNewMessage(ResultView.Mensagem,
                        "Tipo do Veículo não retornado pelo Serviço do Departamento Estadual de Trânsito",
                        MensagemTipoAvisoEnum.Alerta);
                }
            }

            if (ResultView.Mensagem.AvisosImpeditivos.Count + ResultView.Mensagem.Erros.Count > 0)
            {
                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;

                return ResultView;
            }

            #endregion Validações Adicionais

            #region Aplicação das Configurações

            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento = new()
            {
                DataHoraInicialParaCalculo = model.DataHoraInicialParaCalculo.Value,

                DataHoraFinalParaCalculo = model.DataHoraFinalParaCalculo.Value,

                DataHoraPorDeposito = DataHoraPorDeposito,

                FaturarSemGrv = false,

                IsSimulacao = true,

                IsComboio = model.IsComboio,

                StatusOperacaoId = "V",

                IsLeilaoStatus = new[] { "1", "3", "7" }.Contains(Grv.StatusOperacaoId),

                FaturamentoProdutoId = ResultView.Produto.CodigoProduto,

                GrvId = Grv.GrvId,

                NumeroFormularioGrv = Grv.NumeroFormularioGrv,

                TipoVeiculoId = Grv.TipoVeiculoId,

                ClienteDeposito = await _context.ClienteDeposito
                    .Include(x => x.Cliente)
                    .ThenInclude(x => x.Endereco)
                    .Include(x => x.Deposito)
                    .ThenInclude(x => x.Endereco)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x =>
                            x.ClienteId == model.IdentificadorCliente && x.DepositoId == model.IdentificadorDeposito,
                        cancellationToken: ct)
            };

            #endregion Aplicação das Configurações

            CalculoDiariasModel CalculoDiarias = new();

            List<TabelaGenericaModel> ListagemTipoCobranca = await new TabelaGenericaService(_context)
                .ListAsync("FAT_TIPO_COBRANCA");

            FaturamentoModel Faturamento = Faturar(ParametrosCalculoFaturamento, out CalculoDiarias);

            ResultView.Faturamento = _mapper.Map<SimulacaoFaturamentoDTO>(Faturamento);

            ResultView.Faturamento.ListagemServico =
                _mapper.Map<List<SimulacaoFaturamentoComposicaoDTO>>(Faturamento.ListagemFaturamentoComposicao);


            FaturamentoServicoTipoVeiculoModel FaturamentoServicoTipoVeiculo = new();

            foreach (var Servico in ResultView.Faturamento.ListagemServico)
            {
                FaturamentoServicoTipoVeiculo = _context.FaturamentoServicoTipoVeiculo
                    .Include(x => x.FaturamentoServicoAssociado)
                    .ThenInclude(x => x.FaturamentoServicoTipo)
                    .Include(x => x.FaturamentoServicosGrvs)
                    .AsNoTracking()
                    .FirstOrDefault(x =>
                        x.FaturamentoServicoTipoVeiculoId == Servico.IdentificadorFaturamentoServicoTipoVeiculo);


                var servicoGrv = FaturamentoServicoTipoVeiculo?.FaturamentoServicosGrvs
                    ?.FirstOrDefault(x => x.GrvId == Grv.GrvId);

                Servico.IdentificadorServicoGrv = servicoGrv?.FaturamentoServicoGrvId;
                Servico.TempoTrabalhado = servicoGrv?.TempoTrabalhado;

                if (Servico.TipoServico == TipoCobrancaFaturamentoEnum.Horas || Servico.TipoServico == "H")
                {
                    Servico.QuantidadeServico = null;
                }

                Servico.IdentificadorFaturamentoServicoAssociado =
                    FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociadoId;

                Servico.DescricaoTipoServico = ListagemTipoCobranca.Where(x => x.ValorCadastro == Servico.TipoServico)
                    .FirstOrDefault().Descricao;

                Servico.NomeServico = FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.Descricao;

                Servico.DataVigenciaInicial =
                    FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.DataVigenciaInicial;

                Servico.DataVigenciaFinal = FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.DataVigenciaFinal;

                Servico.FlagServicoObrigatorio =
                    FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.FlagServicoObrigatorio == "S" ||
                    FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.FaturamentoServicoTipo
                        ?.FlagServicoObrigatorio == "S"
                        ? "S"
                        : "N";
            }

            ResultView.IdentificadorProcesso = Grv.GrvId;

            ResultView.NumeroProcesso = Grv.NumeroFormularioGrv;
            ResultView.StatusOperacaoId = Grv.StatusOperacaoId;

            ResultView.DataHoraRemocao = Grv.DataHoraRemocao;

            ResultView.DataHoraGuarda = Grv.DataHoraGuarda;

            ResultView.DataHoraInicialParaCalculo = CalculoDiarias.DataHoraInicialParaCalculo;

            ResultView.DataHoraFinalParaCalculo = CalculoDiarias.DataHoraFinalParaCalculo.Value;

            ResultView.QuantidadeDiarias = CalculoDiarias.Diarias;

            ResultView.IdentificadorAtendimento = Grv.Atendimento != null ? Grv.Atendimento.AtendimentoId : 0;

            EnderecoService Endereco = new();

            ResultView.Cliente = new()
            {
                IdentificadorCliente = ParametrosCalculoFaturamento.ClienteDeposito.ClienteId,
                Nome = ParametrosCalculoFaturamento.ClienteDeposito.Cliente.Nome,

                Endereco = Endereco.FormatarEndereco(ParametrosCalculoFaturamento.ClienteDeposito.Cliente.Endereco,
                    ParametrosCalculoFaturamento.ClienteDeposito.Cliente.NumeroEndereco,
                    ParametrosCalculoFaturamento.ClienteDeposito.Cliente.ComplementoEndereco),
                EmitirNota = podeEmitirNota ? true : false
            };

            ResultView.Deposito = new()
            {
                IdentificadorDeposito = ParametrosCalculoFaturamento.ClienteDeposito.ClienteDepositoId,
                Nome = ParametrosCalculoFaturamento.ClienteDeposito.Deposito.Nome,

                Telefone = ParametrosCalculoFaturamento.ClienteDeposito.Deposito.TelefoneMob,

                Endereco = Endereco.FormatarEndereco(ParametrosCalculoFaturamento.ClienteDeposito.Deposito.Endereco,
                    ParametrosCalculoFaturamento.ClienteDeposito.Deposito.NumeroEndereco,
                    ParametrosCalculoFaturamento.ClienteDeposito.Deposito.ComplementoEndereco)
            };

            ResultView.Mensagem = MensagemViewHelper.SetOk();

            return ResultView;
        }

        public async Task<MensagemDTO> UpdateFormaPagamentoAsync(int FaturamentoId, byte TipoMeioCobrancaId,
            int UsuarioId, CancellationToken ct)
        {
            MensagemDTO ResultView = new();

            List<string> erros = new();

            if (FaturamentoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);
            }

            if (TipoMeioCobrancaId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorTipoMeioCobrancaInvalido);
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.SetBadRequest(erros);
            }

            FaturamentoModel Faturamento = await _context.Faturamento
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.Grv)
                .ThenInclude(x => x.Cliente)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FaturamentoId == FaturamentoId, ct);

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
                return MensagemViewHelper.SetBadRequest("Forma de Pagamento já selecionada");
            }

            ResultView = new GrvService(_context)
                .ValidateInputGrv(Faturamento.Atendimento.Grv.GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            TipoMeioCobrancaModel TipoMeioCobranca = await _context.TipoMeioCobranca
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoMeioCobrancaId == TipoMeioCobrancaId, ct);

            if (TipoMeioCobranca == null)
            {
                return MensagemViewHelper.SetBadRequest($"Forma de Pagamento inexistente: {TipoMeioCobrancaId}");
            }
            else if (TipoMeioCobranca.FlagAtivo == "N")
            {
                return MensagemViewHelper.SetBadRequest($"Essa Forma de Pagamento está desativada");
            }
            else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixEstatico &&
                     Faturamento.Atendimento.Grv.Cliente.FlagPossuiPixEstatico == "N")
            {
                return MensagemViewHelper.SetBadRequest("Este Cliente não está configurado para emitir PIX Estático");
            }
            else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixDinamico &&
                     Faturamento.Atendimento.Grv.Cliente.FlagPossuiPixDinamico == "N")
            {
                return MensagemViewHelper.SetBadRequest("Este Cliente não está configurado para emitir PIX Dinâmico");
            }

            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(ct);

            try
            {
                await DeleteTipoMeioCobrancaAtual(FaturamentoId, Faturamento.TipoMeioCobranca, ct);

                await _context.Faturamento
                    .Where(x => x.FaturamentoId == FaturamentoId)
                    .UpdateAsync(x => new FaturamentoModel() { TipoMeioCobrancaId = TipoMeioCobrancaId }, ct);

                await _context.SaveChangesAsync(ct);

                await transaction.CommitAsync(ct);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);

                return MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao alterar a Forma de Pagamento", ex);
            }

            return MensagemViewHelper.SetOk("Forma de Pagamento alterada com sucesso");
        }

        public async Task<FaturamentoDTO> ConfirmarPagamentoAsync(PagamentoParameters parameters, CancellationToken ct)
        {
            FaturamentoDTO ResultView = new();

            List<string> erros = new();

            if (parameters.IdentificadorFaturamento <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);
            }


            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);
                return ResultView;
            }

            FaturamentoModel Faturamento = await _context.Faturamento
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.Grv)
                .ThenInclude(x => x.Cliente)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FaturamentoId == parameters.IdentificadorFaturamento,
                    cancellationToken: ct);

            if (Faturamento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);
                return ResultView;
            }
            else if (Faturamento.Status == "C")
            {
                ResultView.Faturamento = _mapper.Map<FaturamentoCadastroDTO>(Faturamento);
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento foi cancelado");
                return ResultView;
            }
            else if (Faturamento.Status == "P")
            {
                ResultView.Faturamento = _mapper.Map<FaturamentoCadastroDTO>(Faturamento);
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento já foi pago");
                return ResultView;
            }

            ResultView.Faturamento = _mapper.Map<FaturamentoCadastroDTO>(Faturamento);

            ResultView.IdentificadorProcesso = Faturamento.Atendimento.Grv.GrvId;

            ResultView.IdentificadorAtendimento = Faturamento.Atendimento.Grv.Atendimento.AtendimentoId;

            if (Faturamento.Atendimento.Grv
                    .StatusOperacaoId is "L" or "R") // L = AGUARDANDO PAGAMENTO R = Saída Para Reparo
            {
                try
                {
                    TipoMeioCobrancaModel TipoMeioCobranca = await _context.TipoMeioCobranca
                        .FirstOrDefaultAsync(x => x.TipoMeioCobrancaId == Faturamento.TipoMeioCobrancaId, ct);

                    // Se o Tipo de Cobrança for PIX Dinâmico
                    if (TipoMeioCobranca.Alias.Equals("PIXDIN"))
                    {
                        PixDinamicoDTO pixDinamico = new();
                        pixDinamico = await new PixDinamicoService(_context, _mapper, _httpClientFactory)
                            .ConsultaAsync(parameters.IdentificadorFaturamento, parameters.IdentificadorUsuario);
                        if (pixDinamico.IdentificadorPixDinamicoTipoStatusGeracao != 2 &&
                            !parameters.ConfirmoPagamentoComSenha)
                        {
                            var statusPix =
                                await _context.PixDinamicoTipoStatusGeracao
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x =>
                                        x.PixDinamicoTipoStatusGeracaoId ==
                                        pixDinamico.IdentificadorPixDinamicoTipoStatusGeracao, cancellationToken: ct);
                            ResultView.Mensagem = MensagemViewHelper.SetBadRequest(
                                $"Pix ainda não confirmado, status atual: {statusPix.Descricao}");
                            return ResultView;
                        }
                        else
                        {
                            await _context.PixDinamico
                                .Where(x => x.FaturamentoId == parameters.IdentificadorFaturamento)
                                .UpdateAsync(x => new PixDinamicoModel
                                {
                                    PixDinamicoTipoStatusGeracaoId = 2,
                                    DataAlteracao = DateTime.Now
                                }, ct);
                        }
                    }
                    else if (TipoMeioCobranca.Alias.Equals("CCRED") || TipoMeioCobranca.Alias.Equals("CDEBI"))
                    {
                        var faturamentoCartao = await CreateFaturamentoCartao(Faturamento, parameters.Cartoes, ct);

                        if (faturamentoCartao.HtmlStatusCode == HtmlStatusCodeEnum.BadRequest)
                        {
                            ResultView.Mensagem =
                                MensagemViewHelper.SetBadRequest("Erro ao efetuar pagamento com cartão");
                            return ResultView;
                        }
                    }
                    //else if(TipoMeioCobranca.Alias.Equals("GPER"))
                    //{
                    //    //PEMITE PAGAMENTO DIRETO PARA TESTES E APRESENTAÇÕES
                    //}
                    //else
                    //{
                    //    //TODO: Tratar outras formas de pagamento
                    //    ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Forma de pagamento não permitida");
                    //    return ResultView;

                    //}

                    //Atualização do faturamento
                    await _context.Faturamento
                        .Where(x => x.FaturamentoId == parameters.IdentificadorFaturamento)
                        .UpdateAsync(x => new FaturamentoModel()
                        {
                            Status = "P",
                            UsuarioAlteracaoId = parameters.IdentificadorUsuario,
                            DataPrazoRetiradaVeiculo = DateTime.Now.AddDays(1),
                            ValorPagamento = Faturamento.ValorFaturado,
                            DataPagamento = DateTime.Now
                        }, ct);

                    //Atualização da Forma Liberação
                    await _context.Atendimento
                        .Where(x => x.AtendimentoId == Faturamento.AtendimentoId)
                        .UpdateAsync(x => new AtendimentoModel()
                        {
                            FormaLiberacaoNome = Faturamento.Atendimento.ResponsavelNome,
                            FormaLiberacaoCNH = Faturamento.Atendimento.ResponsavelCnh,
                            FormaLiberacaoCPF = Faturamento.Atendimento.ResponsavelDocumento,
                            FormaLiberacao = "C",
                            UsuarioAlteracaoId = parameters.IdentificadorUsuario,
                            FlagPagamentoFinanciado = "N"
                        }, ct);

                    if (!parameters.SaidaParaReparo)
                    {
                        await _context.Grv
                            .Where(x => x.GrvId == Faturamento.Atendimento.GrvId)
                            .UpdateAsync(x => new GrvModel()
                            {
                                StatusOperacaoId = "T",
                                DataAlteracao = DateTime.Now,
                                UsuarioAlteracaoId = parameters.IdentificadorUsuario
                            }, ct);
                    }

                    await _context.SaveChangesAsync(ct);

                    ResultView.Faturamento.Status = "P";
                }
                catch (Exception ex)
                {
                    ResultView.Mensagem = MensagemViewHelper.SetBadRequest(ex.Message);
                    return ResultView;
                }
            }

            return ResultView;
        }

        public async Task<FaturamentoConsultaDTO> ConsultarFaturamentoAsync(int identificadorFaturamento,
            int identificadorUsuario, CancellationToken ct)
        {
            #region Validações dos parâmetros

            List<string> erros = new();

            if (identificadorFaturamento <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);
            }

            FaturamentoConsultaDTO ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            #endregion Validações dos parâmetros

            List<TabelaGenericaModel> ListagemTipoCobranca = await new TabelaGenericaService(_context)
                .ListAsync("FAT_TIPO_COBRANCA");

            #region Consultas

            var Faturamento = await _context.Faturamento
                .AsNoTracking()
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.ListagemFaturamentoComposicao)
                .FirstOrDefaultAsync(x => x.FaturamentoId == identificadorFaturamento, cancellationToken: ct);
            if (Faturamento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Faturamento não encontrado");
                return ResultView;
            }

            var Atendimento = await _context.Atendimento
                .Include(x => x.Grv)
                .ThenInclude(x => x.StatusOperacao)
                .Include(x => x.Grv)
                .ThenInclude(x => x.Cliente)
                .ThenInclude(x => x.Endereco)
                .Include(x => x.Grv)
                .ThenInclude(x => x.Deposito)
                .ThenInclude(x => x.Endereco)
                .Include(x => x.SaidaParaReparo)
                .Include(x => x.UsuarioCadastro)
                .ThenInclude(x => x.Pessoa)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AtendimentoId == Faturamento.AtendimentoId, cancellationToken: ct);
            Faturamento.Atendimento = Atendimento;
            var ListagemFaturamentosAtendimento = await _context.Faturamento
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.ListagemFaturamentoComposicao)
                .Where(x => x.AtendimentoId == Faturamento.AtendimentoId && x.Status != "C")
                .OrderBy(x => x.FaturamentoId)
                .AsNoTracking()
                .ToListAsync(cancellationToken: ct);

            var notas = await _context.Nfe
                .Where(x =>
                    x.GrvId == Faturamento.Atendimento.GrvId &&
                    !_context.Nfe.Any(sb =>
                        sb.GrvId == x.GrvId &&
                        sb.NfeComplementarId == x.NfeId))
                .Select(model => new
                {
                    Nfe = model,
                    Composicoes = model.NfeFaturamentoComposicao
                        .Select(nfc => new
                        {
                            Valor = nfc.FaturamentoComposicao != null
                                ? nfc.FaturamentoComposicao.ValorComposicao
                                : 0,

                            Servico = nfc.FaturamentoComposicao != null &&
                                      nfc.FaturamentoComposicao.FaturamentoServicoTipoVeiculo != null &&
                                      nfc.FaturamentoComposicao.FaturamentoServicoTipoVeiculo
                                          .FaturamentoServicoAssociado != null
                                ? nfc.FaturamentoComposicao
                                    .FaturamentoServicoTipoVeiculo
                                    .FaturamentoServicoAssociado
                                    .Descricao
                                : null
                        })
                        .ToList()
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken: ct);

            LiberacaoEspecialModel liberacaoEspecial = _context.LiberacaoEspecial
                .AsNoTracking()
                .FirstOrDefault(x => x.IdFaturamento == identificadorFaturamento);

            var podeEmitirNota = await _context.FaturamentoRegra
                .AnyAsync(x =>
                    x.ClienteId == Faturamento.Atendimento.Grv.ClienteId &&
                    x.DepositoId == Faturamento.Atendimento.Grv.DepositoId &&
                    x.FaturamentoRegraTipoId == 11, cancellationToken: ct);

            #endregion Consultas

            if (liberacaoEspecial != null)
            {
                ResultView.LiberacaoEspecial = _mapper.Map<LiberacaoEspecialDTO>(liberacaoEspecial);
                ResultView.LiberacaoEspecial.Valor = Faturamento.ValorPagamento ?? 0;
            }

            ResultView.Faturamentos = new List<SimulacaoFaturamentoDTO>();

            foreach (var faturamentoModel in ListagemFaturamentosAtendimento)
            {
                var faturamentoDto = _mapper.Map<SimulacaoFaturamentoDTO>(faturamentoModel);
                faturamentoDto.ListagemServico =
                    _mapper.Map<List<SimulacaoFaturamentoComposicaoDTO>>(faturamentoModel
                        .ListagemFaturamentoComposicao);
                foreach (var Servico in faturamentoDto.ListagemServico)
                {
                    var FaturamentoServicoTipoVeiculo = await _context.FaturamentoServicoTipoVeiculo
                        .Include(x => x.FaturamentoServicoAssociado).ThenInclude(faturamentoServicoAssociadoModel =>
                            faturamentoServicoAssociadoModel.FaturamentoServicoTipo)
                        .Include(x => x.FaturamentoServicosGrvs)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x =>
                                x.FaturamentoServicoTipoVeiculoId == Servico.IdentificadorFaturamentoServicoTipoVeiculo,
                            cancellationToken: ct);
                    var servicoGrv = FaturamentoServicoTipoVeiculo?.FaturamentoServicosGrvs
                        ?.FirstOrDefault(x => x.GrvId == Faturamento.Atendimento.Grv.GrvId);

                    Servico.IdentificadorServicoGrv = servicoGrv?.FaturamentoServicoGrvId;
                    Servico.TempoTrabalhado = servicoGrv?.TempoTrabalhado;

                    if (Servico.TipoServico == TipoCobrancaFaturamentoEnum.Horas || Servico.TipoServico == "H")
                    {
                        Servico.QuantidadeServico = null;
                    }

                    Servico.IdentificadorFaturamentoServicoAssociado =
                        FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociadoId;

                    Servico.DescricaoTipoServico = ListagemTipoCobranca
                        .Where(x => x.ValorCadastro == Servico.TipoServico)
                        .FirstOrDefault()?.Descricao;

                    Servico.NomeServico = FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.Descricao;

                    Servico.DataVigenciaInicial =
                        (DateTime)FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.DataVigenciaInicial;

                    Servico.DataVigenciaFinal =
                        FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.DataVigenciaFinal;

                    Servico.FlagServicoObrigatorio =
                        FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.FlagServicoObrigatorio == "S" ||
                        FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.FaturamentoServicoTipo
                            ?.FlagServicoObrigatorio == "S"
                            ? "S"
                            : "N";
                }

                ResultView.Faturamentos.Add(faturamentoDto);
            }

            ResultView.IdentificadorFaturamento = Faturamento.FaturamentoId;
            ResultView.IdentificadorProcesso = Faturamento.Atendimento.Grv.GrvId;

            ResultView.NumeroProcesso = Faturamento.Atendimento.Grv.NumeroFormularioGrv;

            ResultView.DataHoraRemocao = Faturamento.Atendimento.Grv.DataHoraRemocao;

            ResultView.DataHoraGuarda = Faturamento.Atendimento.Grv.DataHoraGuarda;

            ResultView.IdentificadorAtendimento = Faturamento.AtendimentoId;

            ResultView.StatusOperacaoId = Faturamento.Atendimento.Grv.StatusOperacaoId;
            ResultView.StatusOperacaoDescricao = Faturamento.Atendimento.Grv.StatusOperacao?.Descricao;

            ResultView.TipoMeioCobrancaId = Faturamento.TipoMeioCobrancaId;
            if (notas.Count > 0)
            {
                var notasDto = new List<NFERetornoFaturamentoDTO>();

                var nfeIdentificadoresComErro = notas
                    .Where(x => x.Nfe.Status == "E")
                    .Select(x => x.Nfe.IdentificadorNota)
                    .Distinct()
                    .ToList();

                var erroPorIdentificadorNota = new Dictionary<int, NfeWsErrosModel>();
                if (nfeIdentificadoresComErro.Count > 0)
                {
                    var errosNfe = await _context.NfeWsErros
                        .Where(x =>
                            x.GrvId == Faturamento.Atendimento.GrvId &&
                            x.IdentificadorNota != null &&
                            nfeIdentificadoresComErro.Contains(x.IdentificadorNota.ToString()))
                        .AsNoTracking()
                        .ToListAsync(cancellationToken: ct);

                    erroPorIdentificadorNota = errosNfe
                        .Where(x => x.IdentificadorNota.HasValue)
                        .GroupBy(x => x.IdentificadorNota.Value)
                        .ToDictionary(
                            g => g.Key,
                            g => g
                                .OrderByDescending(x => x.DataHoraCadastro)
                                .First());
                }

                foreach (var item in notas)
                {
                    var nfe = item.Nfe;

                    if (nfe.Status == "E")
                    {
                        var nfDto = _mapper.Map<NFERetornoFaturamentoDTO>(nfe);

                        if (int.TryParse(nfe.IdentificadorNota, out var identificadorNota) &&
                            erroPorIdentificadorNota.TryGetValue(identificadorNota, out var erro))
                        {
                            nfDto.StatusErro = erro.Status;
                            nfDto.MensagemErro = erro.MensagemErro;
                            nfDto.CorrecaoErro = erro.CorrecaoErro;
                        }

                        if (item.Composicoes != null && item.Composicoes.Any())
                        {
                            nfDto.Valor = item.Composicoes.Sum(x => x.Valor);
                            nfDto.Servico = item.Composicoes.Count > 1
                                ? "Vários"
                                : item.Composicoes.FirstOrDefault()?.Servico;
                        }

                        notasDto.Add(nfDto);
                    }
                    else if (item.Composicoes != null && item.Composicoes.Any())
                    {
                        foreach (var composicao in item.Composicoes)
                        {
                            var nfDto = _mapper.Map<NFERetornoFaturamentoDTO>(nfe);

                            nfDto.Valor = composicao.Valor;
                            nfDto.Servico = composicao.Servico;

                            notasDto.Add(nfDto);
                        }
                    }
                    else
                    {
                        notasDto.Add(_mapper.Map<NFERetornoFaturamentoDTO>(nfe));
                    }
                }

                ResultView.NotaFiscal = notasDto;
            }
            else
            {
                ResultView.NotaFiscal = new List<NFERetornoFaturamentoDTO>();
            }

            EnderecoService Endereco = new();

            ResultView.Cliente = new()
            {
                IdentificadorCliente = Faturamento.Atendimento.Grv.Cliente.ClienteId,
                Nome = Faturamento.Atendimento.Grv.Cliente.Nome,

                Endereco = Endereco.FormatarEndereco(Faturamento.Atendimento.Grv.Cliente.Endereco,
                    Faturamento.Atendimento.Grv.Cliente.NumeroEndereco,
                    Faturamento.Atendimento.Grv.Cliente.ComplementoEndereco),
                EmitirNota = podeEmitirNota
            };

            ResultView.Deposito = new()
            {
                IdentificadorDeposito = Faturamento.Atendimento.Grv.Deposito.DepositoId,

                Nome = Faturamento.Atendimento.Grv.Deposito.Nome,

                Telefone = Faturamento.Atendimento.Grv.Deposito.TelefoneMob,

                Endereco = Endereco.FormatarEndereco(Faturamento.Atendimento.Grv.Deposito.Endereco,
                    Faturamento.Atendimento.Grv.Deposito.NumeroEndereco,
                    Faturamento.Atendimento.Grv.Deposito.ComplementoEndereco)
            };
            ResultView.Atendimento = _mapper.Map<AtendimentoDTO>(Faturamento.Atendimento);

            ImageListDTO FotoResponsavel = await new AtendimentoService(_context, _mapper, _httpClientFactory)
                .GetFotoResponsavelAsync(Faturamento.AtendimentoId, identificadorUsuario);

            if (FotoResponsavel.Listagem?.Count > 0)
            {
                ResultView.Atendimento.FotoResponsavel = FotoResponsavel.Listagem
                    .FirstOrDefault()?.Imagem;
            }

            if (!Faturamento.Atendimento.Grv.Placa.IsNullOrWhiteSpace() ||
                !Faturamento.Atendimento.Grv.Chassi.IsNullOrWhiteSpace())
            {
                var detranHubService = _detranHubOptions != null
                    ? new DetranHubService(_httpClientFactory, _mapper, _detranHubOptions)
                    : new DetranHubService(_httpClientFactory, _mapper);

                string placa = Faturamento.Atendimento.Grv.Placa.IsPlaca() ? Faturamento.Atendimento.Grv.Placa : null;
                string chassi = placa == null ? Faturamento.Atendimento.Grv.Chassi : null;

                var detranHubResult = await detranHubService.SearchToPlateOrChassi(placa, chassi);

                if (detranHubResult?.Veiculo != null)
                {
                    ResultView.Veiculo = _mapper.Map<DetranRioVeiculoDTO>(detranHubResult.Veiculo);
                    ResultView.Veiculo.Mensagem = detranHubResult.Mensagem;
                }
                else
                {
                    ResultView.Veiculo = new DetranRioVeiculoDTO
                    {
                        Mensagem = detranHubResult?.Mensagem ?? MensagemViewHelper.SetNotFound("Veículo não encontrado")
                    };
                }
            }

            ResultView.Atendimento.SaidaParaReparo =
                _mapper.Map<AtendimentoSaidaParaReparoDTO>(Faturamento.Atendimento.SaidaParaReparo);

            ResultView.Mensagem = MensagemViewHelper.SetOk();

            return ResultView;
        }

        // public async Task<FaturamentoConsultaDTO> ReprocessarFaturamentoAsync(int identificadorFaturamento, int identificadorUsuario, CancellationToken ct)
        // {
        //     FaturamentoConsultaDTO ResultView = new();
        //
        //     var faturamentoAtual = await _context.Faturamento
        //         .Include(x => x.Atendimento)
        //         .ThenInclude(x => x.Grv)
        //         .ThenInclude(x => x.TipoVeiculo)
        //         .Include(x => x.Atendimento)
        //         .ThenInclude(x => x.Grv)
        //         .ThenInclude(x => x.StatusOperacao)
        //         .FirstOrDefaultAsync(x => x.FaturamentoId == identificadorFaturamento, cancellationToken: ct);
        //
        //     if (faturamentoAtual == null)
        //     {
        //         ResultView.Mensagem = MensagemViewHelper.SetNotFound("Faturamento não encontrado");
        //         return ResultView;
        //     }
        //
        //     if (faturamentoAtual.Status == "P")
        //     {
        //         ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Não é possível reprocessar um faturamento que já foi pago.");
        //         return ResultView;
        //     }
        //
        //     var grv = faturamentoAtual.Atendimento?.Grv;
        //
        //     if (grv == null)
        //     {
        //         ResultView.Mensagem = MensagemViewHelper.SetNotFound("GRV do faturamento não encontrado");
        //         return ResultView;
        //     }
        //
        //     DateTime dataHoraDeposito = new DepositoService(_context).GetDataHoraPorDeposito(grv.DepositoId);
        //
        //     CalculoFaturamentoParametroModel parametrosCalculo = new()
        //     {
        //         DataHoraInicialParaCalculo = grv.DataHoraGuarda!.Value,
        //         DataHoraFinalParaCalculo = dataHoraDeposito != DateTime.MinValue ? dataHoraDeposito : DateTime.Now,
        //         DataHoraPorDeposito = dataHoraDeposito,
        //         FaturarSemGrv = false,
        //         IsSimulacao = false,
        //         IsComboio = false,
        //         StatusOperacaoId = grv.StatusOperacaoId,
        //         IsLeilaoStatus = new[] { "1", "3", "7" }.Contains(grv.StatusOperacaoId),
        //         FaturamentoProdutoId = grv.FaturamentoProdutoId,
        //         GrvId = grv.GrvId,
        //         NumeroFormularioGrv = grv.NumeroFormularioGrv,
        //         TipoVeiculoId = grv.TipoVeiculoId,
        //         TipoMeioCobrancaId = faturamentoAtual.TipoMeioCobrancaId,
        //         ClienteDeposito = await _context.ClienteDeposito
        //             .Include(x => x.Cliente)
        //             .ThenInclude(x => x.Endereco)
        //             .Include(x => x.Deposito)
        //             .ThenInclude(x => x.Endereco)
        //             .AsNoTracking()
        //             .FirstOrDefaultAsync(x => x.ClienteId == grv.ClienteId && x.DepositoId == grv.DepositoId, cancellationToken: ct)
        //     };
        //
        //     FaturamentoModel novoFaturamento = Faturar(parametrosCalculo, out var  calculoDiarias);
        //     novoFaturamento.UsuarioCadastroId = identificadorUsuario;
        //
        //     _context.Faturamento.Add(novoFaturamento);
        //     await _context.SaveChangesAsync(ct);
        //
        //     return await ConsultarFaturamentoAsync(novoFaturamento.FaturamentoId, identificadorUsuario, ct);
        // }

        private async Task<MensagemDTO> CreateFaturamentoCartao(FaturamentoModel faturamento,
            List<PagamentoParameterCartao> cartoes, CancellationToken ct)
        {
            try
            {
                #region Validações dos parâmetros

                if (cartoes == null || !cartoes.Any())
                {
                    return MensagemViewHelper.SetBadRequest("Pelo menos um cartão é obrigatório");
                }

                bool possuiCartoesExistentes =
                    await _context.FaturamentoCodigoAutorizacaoCartao.AnyAsync(x =>
                        x.FaturamentoId == faturamento.FaturamentoId, ct);

                if (possuiCartoesExistentes)
                    return MensagemViewHelper.SetBadRequest("Este faturamento á possui cartôes registrados");

                var cartoesDuplicados = cartoes
                    .GroupBy(x => x.NumeroCartao)
                    .Where(x => !string.IsNullOrWhiteSpace(x.Key) && x.Count() > 1)
                    .Select(x => x.Key)
                    .ToList();
                if (cartoesDuplicados.Any())
                    return MensagemViewHelper.SetBadRequest(
                        $"Existem cartões duplicados com o número: {string.Join(", ", cartoesDuplicados)}");

                #endregion Validações dos parâmetros

                if (cartoes.Count == 1)
                {
                    var cartao = cartoes.First();

                    await _context.FaturamentoCodigoAutorizacaoCartao.AddAsync(
                        new FaturamentoCodigoAutorizacaoCartaoModel()
                        {
                            CartaoId = cartao.Bandeira,
                            CodigoAutorizacaoCartao = cartao.CodigoAutorizacao,
                            NumeroCartao = cartao.NumeroCartao,
                            FaturamentoId = faturamento.FaturamentoId,
                            Valor = faturamento.ValorFaturado
                        }, ct);
                }
                else
                {
                    var cartoesComValor = cartoes.Where(c => c.Valor.HasValue && c.Valor.Value > 0).ToList();
                    var cartoesSemValor = cartoes.Where(c => !c.Valor.HasValue && c.Valor.Value <= 0).ToList();

                    decimal valorTotalCartoes = cartoesComValor.Sum(c => c.Valor.Value);
                    decimal valorRestante = faturamento.ValorFaturado - valorTotalCartoes;


                    if (valorTotalCartoes > faturamento.ValorFaturado)
                        return MensagemViewHelper.SetBadRequest(
                            $"O valor total dos cartões ({valorTotalCartoes:C}) excede o valor do faturamento ({faturamento.ValorFaturado:C})");

                    if (cartoesComValor.Count == cartoes.Count && valorTotalCartoes < faturamento.ValorFaturado)
                        return MensagemViewHelper.SetBadRequest(
                            $"O valor total dos cartões ({valorTotalCartoes:C}) é menor que o valor do faturamento ({faturamento.ValorFaturado:C})");

                    if (cartoesSemValor.Any())
                        return MensagemViewHelper.SetBadRequest(
                            $"O cartão ({cartoesSemValor.Where(x => x.Valor == 0)} precisa ter um valor )"
                        );

                    var cartoesParaAdicionar = cartoes.Select(cartao => new FaturamentoCodigoAutorizacaoCartaoModel
                    {
                        CartaoId = cartao.Bandeira,
                        CodigoAutorizacaoCartao = cartao.CodigoAutorizacao,
                        NumeroCartao = cartao.NumeroCartao,
                        FaturamentoId = faturamento.FaturamentoId,
                        Valor = cartao.Valor.Value
                    }).ToList();

                    if (cartoesParaAdicionar.Any())
                    {
                        decimal somaValores = cartoesParaAdicionar.Sum(x => x.Valor);
                        decimal diferenca = faturamento.ValorFaturado - somaValores;

                        if (Math.Abs(diferenca) > 0.01m)
                        {
                            if (diferenca > 0)
                                return MensagemViewHelper.SetBadRequest(
                                    $"O valor total dos cartões ({somaValores:C}) é menor que o valor do faturamento ({faturamento.ValorFaturado:C}). Faltam {diferenca:C}");
                            else
                                return MensagemViewHelper.SetBadRequest(
                                    $"O valor total dos cartões ({somaValores:C}) excede o valor do faturamento ({faturamento.ValorFaturado:C}). Diferença de {Math.Abs(diferenca):C}");
                        }
                    }

                    await _context.FaturamentoCodigoAutorizacaoCartao.AddRangeAsync(cartoesParaAdicionar, ct);
                }

                await _context.SaveChangesAsync(ct);

                string mensagem = cartoes.Count == 1
                    ? "Faturamento do cartão registrado com sucesso"
                    : $"Faturamento de {cartoes.Count} cartões registrados com sucesso";

                return MensagemViewHelper.SetOk(mensagem);
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetBadRequest("Erro ao registrar faturamento do cartão");
            }
        }

        public async Task<MensagemDTO> GerarFaturamentoSaidaReparoAsync(
            int identificadorProcesso,
            int identificadorSaidaReparo,
            int identificadorUsuario,
            CancellationToken ct = default)
        {
            try
            {
                GrvModel grv = await _context.Grv
                    .Include(x => x.Atendimento)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.GrvId == identificadorProcesso, ct);

                if (grv == null)
                {
                    return MensagemViewHelper.SetNotFound("Processo (GRV) não encontrado.");
                }

                AtendimentoSaidaParaReparoModel saidaReparo = await _context.SaidaReparo
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == identificadorSaidaReparo, ct);

                if (saidaReparo == null)
                {
                    return MensagemViewHelper.SetNotFound("Registro de saída para reparo não encontrado.");
                }

                FaturamentoModel ultimoFaturamento = await _context.Faturamento
                    .AsNoTracking()
                    .Where(x => x.AtendimentoId == grv.Atendimento.AtendimentoId && x.Status != "C")
                    .OrderByDescending(x => x.DataCadastro)
                    .FirstOrDefaultAsync(ct);

                if (ultimoFaturamento == null)
                {
                    return MensagemViewHelper.SetBadRequest(
                        "Nenhum faturamento anterior encontrado para a geração do faturamento adicional.");
                }

                DateTime DataHoraPorDeposito = new DepositoService(_context)
                    .GetDataHoraPorDeposito(grv.DepositoId);
                CalculoFaturamentoParametroModel parametrosCalculo = new()
                {
                    DataHoraInicialParaCalculo = saidaReparo.DataSaida.AddDays(1),
                    DataHoraFinalParaCalculo =
                        DataHoraPorDeposito != DateTime.MinValue ? DataHoraPorDeposito : DateTime.Now,
                    DataHoraPorDeposito = DataHoraPorDeposito,
                    FaturarSemGrv = false,
                    IsSimulacao = false,
                    IsComboio = false,
                    StatusOperacaoId = grv.StatusOperacaoId,
                    IsLeilaoStatus = new[] { "1", "3", "7" }.Contains(grv.StatusOperacaoId),
                    FaturamentoProdutoId = grv.FaturamentoProdutoId,
                    GrvId = grv.GrvId,
                    FaturamentoAdicional = true,
                    NumeroFormularioGrv = grv.NumeroFormularioGrv,
                    TipoVeiculoId = grv.TipoVeiculoId,
                    TipoMeioCobrancaId = ultimoFaturamento.TipoMeioCobrancaId,
                    ClienteDeposito = await _context.ClienteDeposito
                        .Include(x => x.Cliente)
                        .ThenInclude(x => x.Endereco)
                        .Include(x => x.Deposito)
                        .ThenInclude(x => x.Endereco)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(
                            x => x.ClienteId == grv.ClienteId && x.DepositoId == grv.DepositoId,
                            cancellationToken: ct)
                };

                FaturamentoModel faturamentoAdicional = Faturar(parametrosCalculo, out _);
                faturamentoAdicional.UsuarioCadastroId = identificadorUsuario;

                await _context.Faturamento.AddAsync(faturamentoAdicional, ct);
                await _context.SaveChangesAsync(ct);
                return MensagemViewHelper.SetCreateSuccess();
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetInternalServerError(ex.Message);
            }
        }
    }
}
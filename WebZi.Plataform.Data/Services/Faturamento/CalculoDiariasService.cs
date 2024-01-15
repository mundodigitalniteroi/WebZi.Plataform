using Microsoft.EntityFrameworkCore;
using System.Data;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class CalculoDiariasService
    {
        private readonly AppDbContext _context;

        public CalculoDiariasService(AppDbContext context)
        {
            _context = context;
        }

        public CalculoDiariasModel Calcular(CalculoFaturamentoParametroModel ParametrosCalculoDiarias)
        {
            CalculoDiariasModel CalculoDiarias = new()
            {
                ClienteId = ParametrosCalculoDiarias.ClienteDeposito.ClienteId,

                DepositoId = ParametrosCalculoDiarias.ClienteDeposito.DepositoId,

                MaximoDiariasParaCobranca = ParametrosCalculoDiarias.ClienteDeposito.Cliente.MaximoDiariasParaCobranca,

                MaximoDiasVencimento = ParametrosCalculoDiarias.ClienteDeposito.Cliente.MaximoDiasVencimento,

                HoraDiaria = ParametrosCalculoDiarias.ClienteDeposito.Cliente.HoraDiaria,

                DataHoraInicialParaCalculo = ParametrosCalculoDiarias.DataHoraInicialParaCalculo,

                DataHoraFinalParaCalculo = ParametrosCalculoDiarias.DataHoraFinalParaCalculo,

                FlagClienteRealizaFaturamentoArrecadacao = ParametrosCalculoDiarias.ClienteDeposito.Cliente.FlagClienteRealizaFaturamentoArrecadacao == "S",

                FlagUsarHoraDiaria = ParametrosCalculoDiarias.ClienteDeposito.Cliente.FlagUsarHoraDiaria == "S",

                FlagEmissaoNotaFiscalERP = ParametrosCalculoDiarias.ClienteDeposito.Cliente.FlagEmissaoNotaFiscal == "S",

                FlagCobrarDiariasDiasCorridos = ParametrosCalculoDiarias.ClienteDeposito.Cliente.FlagCobrarDiariasDiasCorridos == "S",

                IsComboio = ParametrosCalculoDiarias.IsComboio,

                IsPrimeiroFaturamento = (new[] { "V", "1", "B", "D" }.Contains(ParametrosCalculoDiarias.StatusOperacaoId)),
            };

            if (!CalculoDiarias.DataHoraFinalParaCalculo.HasValue || CalculoDiarias.DataHoraFinalParaCalculo == DateTime.MinValue)
            {
                CalculoDiarias.DataHoraFinalParaCalculo = ParametrosCalculoDiarias.DataHoraPorDeposito;
            }

            #region REGRA DA HORA DA VIRADA DA DIÁRIA
            // Data Hora Configuração contém a hora limite da virada para a contagem de mais uma diária, geralmente meio-dia
            // do contrário será usada a hora da guarda do veículo
            if (CalculoDiarias.FlagUsarHoraDiaria)
            {
                CalculoDiarias.DataHoraDiaria = new DateTime
                (
                    CalculoDiarias.DataHoraFinalParaCalculo.Value.Year,
                    CalculoDiarias.DataHoraFinalParaCalculo.Value.Month,
                    CalculoDiarias.DataHoraFinalParaCalculo.Value.Day,
                    int.Parse(StringHelper.Left(CalculoDiarias.HoraDiaria, 2)),
                    int.Parse(StringHelper.Right(CalculoDiarias.HoraDiaria, 2)),
                    0
                );
            }
            else
            {
                CalculoDiarias.DataHoraDiaria = new DateTime
                (
                    CalculoDiarias.DataHoraFinalParaCalculo.Value.Year,
                    CalculoDiarias.DataHoraFinalParaCalculo.Value.Month,
                    CalculoDiarias.DataHoraFinalParaCalculo.Value.Day,
                    CalculoDiarias.DataHoraInicialParaCalculo.Hour,
                    CalculoDiarias.DataHoraInicialParaCalculo.Minute,
                    0
                );
            }
            #endregion REGRA DA HORA DA VIRADA DA DIÁRIA

            List<FaturamentoRegraModel> RegrasFaturamento = _context.FaturamentoRegra
                .Include(x => x.FaturamentoRegraTipo)
                .Where(x => x.ClienteId == CalculoDiarias.ClienteId && x.DepositoId == CalculoDiarias.DepositoId)
                .AsNoTracking()
                .ToList();

            if (RegrasFaturamento?.Count == 0)
            {
                RegrasFaturamento = null;
            }

            CalculoDiarias = GetLocalizacaoDeposito(CalculoDiarias);

            CalculoDiarias.DataHoraOntem = CalculoDiarias.DataHoraFinalParaCalculo.Value.AddDays(-1);

            CalculoDiarias.DataHoraOntem = new DateTime(CalculoDiarias.DataHoraOntem.Year,
                                                        CalculoDiarias.DataHoraOntem.Month,
                                                        CalculoDiarias.DataHoraOntem.Day,
                                                        23,
                                                        59,
                                                        59);

            CalculoDiarias.Feriados = new FeriadoService(_context)
                .ListDataFeriado(CalculoDiarias.UF,
                                         CalculoDiarias.MunicipioId,
                                         CalculoDiarias.DataHoraInicialParaCalculo.Date.Year,
                                         CalculoDiarias.DataHoraDiaria.Date.AddDays(10).Year);

            CalculoDiarias.QuantidadeDiariasPagas = GetQuantidadeDiariasPagas(CalculoDiarias);

            CalculoDiarias.FlagCobrarTodasDiarias = RegrasFaturamento?.Count > 0 &&
                RegrasFaturamento.Any(w => w.FaturamentoRegraTipo.Codigo == FaturamentoRegraTipoEnum.CalculoDiasNaoCobradas);

            if (CalculoDiarias.MaximoDiariasParaCobranca > 0 &&
                CalculoDiarias.QuantidadeDiariasPagas > 0 &&
                CalculoDiarias.FlagCobrarTodasDiarias)
            {
                // Regra calcular diarias não cobradas para faturamento adicional - a partir da data da guarda
                CalculoDiarias.Diarias = DateTimeHelper.GetDaysBetweenTwoDates(CalculoDiarias.DataHoraInicialParaCalculo.Date, CalculoDiarias.DataHoraDiaria.Date);

                CalculoDiarias.Diarias -= CalculoDiarias.QuantidadeDiariasPagas;

                if (CalculoDiarias.Diarias <= 0)
                {
                    CalculoDiarias.Diarias = 1;
                }
            }
            else
            {
                // Não conta o dia da Data Inicial
                CalculoDiarias.Diarias = DateTimeHelper.GetDaysBetweenTwoDates(CalculoDiarias.DataHoraInicialParaCalculo.Date, CalculoDiarias.DataHoraDiaria.Date);
            }

            CalculoDiarias.QuantidadeDiasUteis = FeriadoService.GetDiasUteis(CalculoDiarias.DataHoraInicialParaCalculo.Date,
                                                              CalculoDiarias.DataHoraOntem.Date,
                                                              CalculoDiarias.Feriados);

            // Se a data da guarda for hoje
            if ((CalculoDiarias.DataHoraInicialParaCalculo.Date == CalculoDiarias.DataHoraDiaria.Date) &&
                (CalculoDiarias.DataHoraInicialParaCalculo.Date == CalculoDiarias.DataHoraFinalParaCalculo.Value.Date))
            {
                CalculoDiarias.Diarias = 1;
            }
            // Ocorre quando só tem dias úteis
            else if (CalculoDiarias.Diarias == CalculoDiarias.QuantidadeDiasUteis)
            {
                if ((CalculoDiarias.DataHoraFinalParaCalculo.Value.Hour > CalculoDiarias.DataHoraDiaria.Hour) ||
                   ((CalculoDiarias.DataHoraFinalParaCalculo.Value.Hour == CalculoDiarias.DataHoraDiaria.Hour) &&
                   (CalculoDiarias.DataHoraFinalParaCalculo.Value.Minute > CalculoDiarias.DataHoraDiaria.Minute)))
                {
                    CalculoDiarias.Diarias++;
                }
            }
            // Dias corridos, ignora dias não úteis e calcula a partir da Data da Guarda.
            else if (CalculoDiarias.FlagCobrarDiariasDiasCorridos)
            {
                if ((CalculoDiarias.DataHoraFinalParaCalculo.Value.Hour > CalculoDiarias.DataHoraDiaria.Hour) ||
                   ((CalculoDiarias.DataHoraFinalParaCalculo.Value.Hour == CalculoDiarias.DataHoraDiaria.Hour) &&
                   (CalculoDiarias.DataHoraFinalParaCalculo.Value.Minute > CalculoDiarias.DataHoraDiaria.Minute)))
                {
                    CalculoDiarias.Diarias++;
                }
            }
            // Ocorre quando a Data da Guarda foi num dia útil mas ontem não foi dia útil
            else if (FeriadoService.IsDiaUtil(CalculoDiarias.DataHoraInicialParaCalculo, CalculoDiarias.Feriados) && (!FeriadoService.IsDiaUtil(CalculoDiarias.DataHoraOntem.Date, CalculoDiarias.Feriados)) && (CalculoDiarias.QuantidadeDiasUteis <= 1))
            {
                int dias = CalculoDiarias.Diarias;

                // Caso a hora do faturamento for superior à hora definida na configuração HRLIBDIAUTIL, todas as diárias serão cobradas
                // Esta regra só é válida quando o faturamento for após um dia não útil
                CalculoDiarias.Diarias = GetAdicionarDiaRegraFaturamento(RegrasFaturamento, CalculoDiarias.DataHoraFinalParaCalculo.Value, CalculoDiarias.Diarias);

                if (CalculoDiarias.Diarias == dias)
                {
                    CalculoDiarias.Diarias = 1;
                }
            }
            // Ocorre quando a guarda ocorre num dia não útil
            else if (!FeriadoService.IsDiaUtil(CalculoDiarias.DataHoraInicialParaCalculo, CalculoDiarias.Feriados) && !FeriadoService.IsDiaUtil(CalculoDiarias.DataHoraOntem, CalculoDiarias.Feriados) && (CalculoDiarias.QuantidadeDiasUteis <= 1))
            {
                if (!FeriadoService.IsDiaUtil(CalculoDiarias.DataHoraOntem.Date, CalculoDiarias.Feriados))
                {
                    int dias = CalculoDiarias.Diarias;

                    CalculoDiarias.Diarias = GetAdicionarDiaRegraFaturamento(RegrasFaturamento, CalculoDiarias.DataHoraFinalParaCalculo.Value, CalculoDiarias.Diarias);

                    if (CalculoDiarias.Diarias == dias)
                    {
                        CalculoDiarias.Diarias = 1;
                    }
                }
                else
                {
                    CalculoDiarias.Diarias++;
                }
            }
            // Ocorre quando existe mais de 1 dia útil
            else if (CalculoDiarias.QuantidadeDiasUteis >= 1)
            {
                if ((CalculoDiarias.DataHoraFinalParaCalculo.Value.Hour > CalculoDiarias.DataHoraDiaria.Hour) ||
                   ((CalculoDiarias.DataHoraFinalParaCalculo.Value.Hour == CalculoDiarias.DataHoraDiaria.Hour) &&
                   (CalculoDiarias.DataHoraFinalParaCalculo.Value.Minute > CalculoDiarias.DataHoraDiaria.Minute)))
                {
                    CalculoDiarias.Diarias++;
                }
            }

            int totalRealDias = CalculoDiarias.Diarias;

            CalculoDiarias.Diarias = GetMaximoDiaCobranca(CalculoDiarias.MaximoDiariasParaCobranca, CalculoDiarias.Diarias, CalculoDiarias.QuantidadeDiariasPagas);

            if (ParametrosCalculoDiarias.FaturamentoAdicional)
            {
                CalculoDiarias.Diarias = GetMaximoDiariasCobrarFaturaAdicional(RegrasFaturamento, CalculoDiarias.Diarias);
            }

            CalculoDiarias.Diarias = GetRegraNaoCobrarDiariaAtual(RegrasFaturamento, CalculoDiarias, CalculoDiarias.Feriados, totalRealDias, CalculoDiarias.Diarias);

            CalculoDiarias.Diarias = GetRegraNaoCobrarPrimeiraDiaria(RegrasFaturamento, CalculoDiarias, CalculoDiarias.Diarias);

            if (CalculoDiarias.Diarias <= 0)
            {
                CalculoDiarias.Diarias = 1;
            }

            return CalculoDiarias;
        }

        private CalculoDiariasModel GetLocalizacaoDeposito(CalculoDiariasModel CalculoDiarias)
        {
            DepositoModel Deposito = _context.Deposito
                .Include(x => x.Endereco)
                .Where(x => x.DepositoId == CalculoDiarias.DepositoId)
                .AsNoTracking()
                .FirstOrDefault();

            if (Deposito != null)
            {
                CalculoDiarias.UF = Deposito.Endereco.UF;

                CalculoDiarias.MunicipioId = Deposito.Endereco.MunicipioId;
            }

            return CalculoDiarias;
        }

        private int GetQuantidadeDiariasPagas(CalculoDiariasModel CalculoDiarias)
        {
            if (!CalculoDiarias.IsPrimeiroFaturamento)
            {
                List<FaturamentoComposicaoModel> FaturamentoComposicoes = _context.FaturamentoComposicao
                    .Include(x => x.Faturamento)
                    .Where(x => x.TipoComposicao == TipoCobrancaFaturamentoEnum.Diárias &&
                                x.QuantidadeComposicao > 0 &&
                                x.Faturamento.AtendimentoId == CalculoDiarias.AtendimentoId &&
                                x.Faturamento.Status == "P")
                    .ToList();

                if (FaturamentoComposicoes?.Count > 0)
                {
                    return (int)FaturamentoComposicoes
                        .Sum(s => s.QuantidadeComposicao);
                }
            }

            return 0;
        }

        private static int GetAdicionarDiaRegraFaturamento(List<FaturamentoRegraModel> FaturamentosRegras, DateTime dataFinal, int dias)
        {
            if (FaturamentosRegras?.Count > 0)
            {
                FaturamentoRegraModel FaturamentoRegra = FaturamentosRegras
                    .Find(s => s.FaturamentoRegraTipo.Codigo == FaturamentoRegraTipoEnum.HoraLiberacaoPrimeiroDiaUtil);

                if (FaturamentoRegra != null)
                {
                    int hora = int.Parse(FaturamentoRegra.Valor);

                    if (dataFinal.Hour >= hora)
                    {
                        dias++;
                    }
                }
            }

            return dias;
        }

        private static int GetMaximoDiaCobranca(int maximoDiariasParaCobranca, int dias, int diariasPagas)
        {
            int diasResultantes = maximoDiariasParaCobranca - diariasPagas;

            if (diasResultantes <= 0)
            {
                diasResultantes = 1;
            }

            if (dias > diasResultantes)
            {
                return diasResultantes;
            }

            if (maximoDiariasParaCobranca > 0)
            {
                // Se a quantidade de dias calculados for maior do que a quantidade máxima de diárias cadastrada
                if (dias > maximoDiariasParaCobranca)
                {
                    dias = maximoDiariasParaCobranca;
                }
            }

            return dias;
        }

        private static int GetMaximoDiariasCobrarFaturaAdicional(List<FaturamentoRegraModel> FaturamentoRegras, int dias)
        {
            if (FaturamentoRegras?.Count > 0)
            {
                FaturamentoRegraModel FaturamentoRegra = FaturamentoRegras
                    .Find(s => s.FaturamentoRegraTipo.Codigo == FaturamentoRegraTipoEnum.MaximoDiariasCobrancaFaturaAdicional);

                if (FaturamentoRegra != null)
                {
                    int diasMax = int.Parse(FaturamentoRegra.Valor);

                    if (dias > diasMax)
                    {
                        return diasMax;
                    }
                }
            }

            return dias;
        }

        private static int GetRegraNaoCobrarDiariaAtual(List<FaturamentoRegraModel> FaturamentoRegras, CalculoDiariasModel CalculoDiarias, List<DateTime> Feriados, int totalRealDias, int dias)
        {
            if (!CalculoDiarias.IsComboio &&
                CalculoDiarias.IsPrimeiroFaturamento &&
                FaturamentoRegras?.Count > 0 &&
                dias > 1 &&
                FaturamentoRegras.Any(w => w.FaturamentoRegraTipo.Codigo == FaturamentoRegraTipoEnum.NaoCobrarDiariaDiaAtualQuandoQuantidadeDiariasMaiorQueUm) &&
                FeriadoService.IsDiaUtil(CalculoDiarias.DataHoraFinalParaCalculo.Value, Feriados) &&
                CalculoDiarias.DataHoraFinalParaCalculo.Value.Hour > CalculoDiarias.DataHoraDiaria.Hour &&
                (CalculoDiarias.MaximoDiariasParaCobranca > 0 && CalculoDiarias.MaximoDiariasParaCobranca >= totalRealDias))
            {
                dias--;
            }

            return dias;
        }

        private static int GetRegraNaoCobrarPrimeiraDiaria(List<FaturamentoRegraModel> FaturamentoRegras, CalculoDiariasModel CalculoDiarias, int dias)
        {
            if (!CalculoDiarias.IsComboio &&
                CalculoDiarias.IsPrimeiroFaturamento &&
                FaturamentoRegras?.Count > 0
                && FaturamentoRegras.Any(w => w.FaturamentoRegraTipo.Codigo == FaturamentoRegraTipoEnum.NaoCobrarPrimeiraDiaria))
            {
                dias--;
            }

            return dias;
        }
    }
}
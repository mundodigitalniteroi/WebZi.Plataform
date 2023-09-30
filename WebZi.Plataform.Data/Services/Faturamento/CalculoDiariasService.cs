using Microsoft.EntityFrameworkCore;
using System.Data;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Database;
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

        public CalculoDiariasModel Calcular(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            CalculoDiariasModel CalculoDiarias = new()
            {
                ClienteId = ParametrosCalculoFaturamento.Grv.ClienteId,

                DepositoId = ParametrosCalculoFaturamento.Grv.DepositoId,

                MaximoDiariasParaCobranca = ParametrosCalculoFaturamento.Grv.Cliente.MaximoDiariasParaCobranca,

                MaximoDiasVencimento = ParametrosCalculoFaturamento.Grv.Cliente.MaximoDiasVencimento,

                HoraDiaria = ParametrosCalculoFaturamento.Grv.Cliente.HoraDiaria,

                DataHoraInicialParaCalculo = ParametrosCalculoFaturamento.Grv.DataHoraGuarda,

                FlagClienteRealizaFaturamentoArrecadacao = ParametrosCalculoFaturamento.Grv.Cliente.FlagClienteRealizaFaturamentoArrecadacao,

                FlagUsarHoraDiaria = ParametrosCalculoFaturamento.Grv.Cliente.FlagUsarHoraDiaria,

                FlagEmissaoNotaFiscalSap = ParametrosCalculoFaturamento.Grv.Cliente.FlagEmissaoNotaFiscal,

                FlagCobrarDiariasDiasCorridos = ParametrosCalculoFaturamento.Grv.Cliente.FlagCobrarDiariasDiasCorridos,

                FlagComboio = ParametrosCalculoFaturamento.Grv.FlagComboio,

                FlagPrimeiroFaturamento = (new[] { "V", "1", "B", "D" }.Contains(ParametrosCalculoFaturamento.Grv.StatusOperacaoId)) ? "S" : "N",
            };

            if (!CalculoDiarias.DataHoraFinalParaCalculo.HasValue || CalculoDiarias.DataHoraFinalParaCalculo == DateTime.MinValue)
            {
                CalculoDiarias.DataHoraFinalParaCalculo = ParametrosCalculoFaturamento.DataHoraPorDeposito;
            }

            #region REGRA DA HORA DA VIRADA DA DIÁRIA
            // Data Hora Configuração contém a hora limite da virada para a contagem de mais uma diária, geralmente meio-dia
            // do contrário será usada a hora da guarda do veículo
            if (CalculoDiarias.FlagUsarHoraDiaria == "S")
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
                .Where(w => w.ClienteId == CalculoDiarias.ClienteId && w.DepositoId == CalculoDiarias.DepositoId)
                .AsNoTracking()
                .ToList();

            if (RegrasFaturamento.Count == 0)
            {
                RegrasFaturamento = null;
            }

            CalculoDiarias = SelecionarLocalizacaoDeposito(CalculoDiarias);

            CalculoDiarias.DataHoraOntem = CalculoDiarias.DataHoraFinalParaCalculo.Value.AddDays(-1);

            CalculoDiarias.DataHoraOntem = new DateTime(CalculoDiarias.DataHoraOntem.Year, CalculoDiarias.DataHoraOntem.Month, CalculoDiarias.DataHoraOntem.Day, 23, 59, 59);

            CalculoDiarias.Feriados = new FeriadoService(_context)
                .SelecionarDatasFeriados(CalculoDiarias.Uf, CalculoDiarias.MunicipioId, CalculoDiarias.DataHoraInicialParaCalculo.Date.Year, CalculoDiarias.DataHoraDiaria.Date.AddDays(10).Year);

            CalculoDiarias.QuantidadeDiariasPagas = QuantidadeDiariasPagas(CalculoDiarias);

            CalculoDiarias.CobrarTodasDiarias = RegrasFaturamento?.Count > 0 && RegrasFaturamento.Any(w => w.FaturamentoRegraTipo.Codigo == "CALCDIASNAOCOBRADAS");

            if (CalculoDiarias.MaximoDiariasParaCobranca > 0 &&
                CalculoDiarias.QuantidadeDiariasPagas > 0 &&
                CalculoDiarias.CobrarTodasDiarias)
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

            CalculoDiarias.QuantidadeDiasUteis = GetDiasUteis(CalculoDiarias.DataHoraInicialParaCalculo.Date, CalculoDiarias.DataHoraOntem.Date, CalculoDiarias.Feriados);

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
            else if (CalculoDiarias.FlagCobrarDiariasDiasCorridos == "S")
            {
                if ((CalculoDiarias.DataHoraFinalParaCalculo.Value.Hour > CalculoDiarias.DataHoraDiaria.Hour) ||
                   ((CalculoDiarias.DataHoraFinalParaCalculo.Value.Hour == CalculoDiarias.DataHoraDiaria.Hour) &&
                   (CalculoDiarias.DataHoraFinalParaCalculo.Value.Minute > CalculoDiarias.DataHoraDiaria.Minute)))
                {
                    CalculoDiarias.Diarias++;
                }
            }
            // Ocorre quando a Data da Guarda foi num dia útil mas ontem não foi dia útil
            else if (IsDiaUtil(CalculoDiarias.DataHoraInicialParaCalculo, CalculoDiarias.Feriados) && (!IsDiaUtil(CalculoDiarias.DataHoraOntem.Date, CalculoDiarias.Feriados)) && (CalculoDiarias.QuantidadeDiasUteis <= 1))
            {
                int dias = CalculoDiarias.Diarias;

                // Caso a hora do faturamento for superior à hora definida na configuração HRLIBDIAUTIL, todas as diárias serão cobradas
                // Esta regra só é válida quando o faturamento for após um dia não útil
                CalculoDiarias.Diarias = AdicionarDiaRegraFaturamento(RegrasFaturamento, CalculoDiarias.DataHoraFinalParaCalculo.Value, CalculoDiarias.Diarias);

                if (CalculoDiarias.Diarias == dias)
                {
                    CalculoDiarias.Diarias = 1;
                }
            }
            // Ocorre quando a guarda ocorre num dia não útil
            else if (!IsDiaUtil(CalculoDiarias.DataHoraInicialParaCalculo, CalculoDiarias.Feriados) && !IsDiaUtil(CalculoDiarias.DataHoraOntem, CalculoDiarias.Feriados) && (CalculoDiarias.QuantidadeDiasUteis <= 1))
            {
                if (!IsDiaUtil(CalculoDiarias.DataHoraOntem.Date, CalculoDiarias.Feriados))
                {
                    int dias = CalculoDiarias.Diarias;

                    CalculoDiarias.Diarias = AdicionarDiaRegraFaturamento(RegrasFaturamento, CalculoDiarias.DataHoraFinalParaCalculo.Value, CalculoDiarias.Diarias);

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

            CalculoDiarias.Diarias = RetornarMaximoDiaCobranca(CalculoDiarias.MaximoDiariasParaCobranca, CalculoDiarias.Diarias, CalculoDiarias.QuantidadeDiariasPagas);

            if (ParametrosCalculoFaturamento.FaturamentoAdicional)
            {
                CalculoDiarias.Diarias = MaximoDiariasCobrarFaturaAdicional(RegrasFaturamento, CalculoDiarias.Diarias);
            }

            CalculoDiarias.Diarias = RegraNaoCobrarDiariaAtual(RegrasFaturamento, CalculoDiarias, CalculoDiarias.Feriados, totalRealDias, CalculoDiarias.Diarias);

            CalculoDiarias.Diarias = VerificarRegraNaoCobrarPrimeiraDiaria(RegrasFaturamento, CalculoDiarias, CalculoDiarias.Diarias);

            if (CalculoDiarias.Diarias <= 0)
            {
                CalculoDiarias.Diarias = 1;
            }

            return CalculoDiarias;
        }

        private CalculoDiariasModel SelecionarLocalizacaoDeposito(CalculoDiariasModel CalculoDiarias)
        {
            DepositoModel Deposito = _context.Deposito
                .Include(i => i.CEP)
                .ThenInclude(t => t.Municipio)
                .Where(w => w.DepositoId == CalculoDiarias.DepositoId)
                .AsNoTracking()
                .FirstOrDefault();

            if (Deposito != null)
            {
                CalculoDiarias.Uf = Deposito.CEP.Municipio.Uf;

                CalculoDiarias.MunicipioId = Deposito.CEP.Municipio.MunicipioId;
            }

            return CalculoDiarias;
        }

        private int QuantidadeDiariasPagas(CalculoDiariasModel CalculoDiarias)
        {
            if (CalculoDiarias.FlagPrimeiroFaturamento == "N")
            {
                List<FaturamentoComposicaoModel> FaturamentoComposicoes = _context.FaturamentoComposicao
                    .Include(i => i.Faturamento)
                    .Where(w => w.TipoComposicao == "D" &&
                                w.QuantidadeComposicao > 0 &&
                                w.Faturamento.AtendimentoId == CalculoDiarias.AtendimentoId &&
                                w.Faturamento.Status == "P")
                    .ToList();

                if (FaturamentoComposicoes?.Count > 0)
                {
                    return (int)FaturamentoComposicoes
                        .Sum(s => s.QuantidadeComposicao);
                }
            }

            return 0;
        }

        private static int GetDiasUteis(DateTime dataInicial, DateTime dataFinal, List<DateTime> Feriados)
        {
            int diasUteis = 0;

            int dias = DateTimeHelper.GetDaysBetweenTwoDates(dataInicial.Date, dataFinal.Date) + 1;

            if (dataInicial > dataFinal)
            {
                return 0;
            }

            for (int i = 1; i <= dias; i++)
            {
                if (IsDiaUtil(dataInicial, Feriados))
                {
                    diasUteis++;
                }

                dataInicial = dataInicial.AddDays(1);
            }

            return diasUteis;
        }

        private static bool IsDiaUtil(DateTime inputDate, List<DateTime> Feriados)
        {
            if (inputDate.DayOfWeek == DayOfWeek.Saturday || inputDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            if (HolidayHelper.IsHoliday(inputDate))
            {
                return false;
            }

            if (Feriados?.Count > 0)
            {
                DateTime result = Feriados.Find(w => w.Date == inputDate.Date);

                if (result > DateTime.MinValue)
                {
                    return false;
                }
            }

            return true;
        }

        private static int AdicionarDiaRegraFaturamento(List<FaturamentoRegraModel> FaturamentosRegras, DateTime dataFinal, int dias)
        {
            if (FaturamentosRegras?.Count > 0)
            {
                FaturamentoRegraModel FaturamentoRegra = FaturamentosRegras
                    .Find(s => s.FaturamentoRegraTipo.Codigo == "HRLIBDIAUTIL");

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

        private static int RetornarMaximoDiaCobranca(int maximoDiariasParaCobranca, int dias, int diariasPagas)
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

        private static int MaximoDiariasCobrarFaturaAdicional(List<FaturamentoRegraModel> FaturamentoRegras, int dias)
        {
            if (FaturamentoRegras?.Count > 0)
            {
                FaturamentoRegraModel FaturamentoRegra = FaturamentoRegras
                    .Find(s => s.FaturamentoRegraTipo.Codigo == "MAXDIASFATADICIONAL");

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

        private static int RegraNaoCobrarDiariaAtual(List<FaturamentoRegraModel> FaturamentoRegras, CalculoDiariasModel CalculoDiarias, List<DateTime> Feriados, int totalRealDias, int dias)
        {
            if (CalculoDiarias.FlagComboio == "N" &&
                CalculoDiarias.FlagPrimeiroFaturamento == "S" &&
                FaturamentoRegras?.Count > 0 &&
                dias > 1 &&
                FaturamentoRegras.Any(w => w.FaturamentoRegraTipo.Codigo == "NAOCOBRARDIARIAATUAL") &&
                IsDiaUtil(CalculoDiarias.DataHoraFinalParaCalculo.Value, Feriados) &&
                CalculoDiarias.DataHoraFinalParaCalculo.Value.Hour > CalculoDiarias.DataHoraDiaria.Hour &&
                (CalculoDiarias.MaximoDiariasParaCobranca > 0 && CalculoDiarias.MaximoDiariasParaCobranca >= totalRealDias))
            {
                dias--;
            }

            return dias;
        }

        private static int VerificarRegraNaoCobrarPrimeiraDiaria(List<FaturamentoRegraModel> FaturamentoRegras, CalculoDiariasModel CalculoDiarias, int dias)
        {
            if (CalculoDiarias.FlagComboio == "N" &&
                CalculoDiarias.FlagPrimeiroFaturamento == "S" &&
                FaturamentoRegras?.Count > 0
                && FaturamentoRegras.Any(w => w.FaturamentoRegraTipo.Codigo == "NAOCOBRPRIMEIRDIARIA"))
            {
                dias--;
            }

            return dias;
        }
    }
}
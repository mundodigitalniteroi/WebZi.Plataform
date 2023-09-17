using Microsoft.EntityFrameworkCore;
using System.Data;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class CalculoDiariasService
    {
        private readonly AppDbContext _context;

        public CalculoDiariasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CalculoDiariasModel> Calcular(GrvModel Grv, DateTime DataHoraAtualPorDeposito, bool FaturamentoAdicional = false)
        {
            CalculoDiariasModel CalculoDiarias = new()
            {
                ClienteId = Grv.ClienteId,

                DepositoId = Grv.DepositoId,

                MaximoDiariasParaCobranca = Grv.Cliente.MaximoDiariasParaCobranca,

                MaximoDiasVencimento = Grv.Cliente.MaximoDiasVencimento,

                HoraDiaria = Grv.Cliente.HoraDiaria,

                IsClienteRealizaFaturamentoArrecadacao = Grv.Cliente.FlagClienteRealizaFaturamentoArrecadacao,

                FlagUsarHoraDiaria = Grv.Cliente.FlagUsarHoraDiaria,

                IsEmissaoNotaFiscalSap = Grv.Cliente.FlagEmissaoNotaFiscalSap,

                IsCobrarDiariasDiasCorridos = Grv.Cliente.FlagCobrarDiariasDiasCorridos,

                DataHoraGuarda = Grv.DataHoraGuarda.GetValueOrDefault(),

                DataHoraInicialParaCalculo = Grv.DataHoraGuarda.GetValueOrDefault(),

                IsComboio = Grv.FlagComboio,

                IsPrimeiroFaturamento = (new[] { "V", "1", "B", "D" }.Contains(Grv.StatusOperacaoId)) ? "S" : "N",
            };

            if (!CalculoDiarias.DataFinalParaCalculo.HasValue || CalculoDiarias.DataFinalParaCalculo == DateTime.MinValue)
            {
                CalculoDiarias.DataFinalParaCalculo = DataHoraAtualPorDeposito;
            }

            #region REGRA DA HORA DA VIRADA DA DIÁRIA
            // Data Hora Configuração contém a hora limite da virada para a contagem de mais uma diária, geralmente meio-dia
            // do contrário será usada a hora da guarda do veículo
            if (CalculoDiarias.FlagUsarHoraDiaria == "S")
            {
                CalculoDiarias.DataHoraDiaria = new DateTime
                (
                    CalculoDiarias.DataFinalParaCalculo.Value.Year,
                    CalculoDiarias.DataFinalParaCalculo.Value.Month,
                    CalculoDiarias.DataFinalParaCalculo.Value.Day,
                    int.Parse(StringHelper.Left(CalculoDiarias.HoraDiaria, 2)),
                    int.Parse(StringHelper.Right(CalculoDiarias.HoraDiaria, 2)),
                    0
                );
            }
            else
            {
                CalculoDiarias.DataHoraDiaria = new DateTime
                (
                    CalculoDiarias.DataFinalParaCalculo.Value.Year,
                    CalculoDiarias.DataFinalParaCalculo.Value.Month,
                    CalculoDiarias.DataFinalParaCalculo.Value.Day,
                    CalculoDiarias.DataHoraInicialParaCalculo.Hour,
                    CalculoDiarias.DataHoraInicialParaCalculo.Minute,
                    0
                );
            }
            #endregion REGRA DA HORA DA VIRADA DA DIÁRIA

            List<FaturamentoRegraModel> RegrasFaturamento = await _context.FaturamentoRegras
                .Where(w => w.ClienteId.Equals(CalculoDiarias.ClienteId) && w.DepositoId.Equals(CalculoDiarias.DepositoId))
                .AsNoTracking()
                .ToListAsync();

            if (RegrasFaturamento.Count.Equals(0))
            {
                RegrasFaturamento = null;
            }

            CalculoDiarias = await SelecionarLocalizacaoDeposito(CalculoDiarias);

            CalculoDiarias.DataHoraOntem = CalculoDiarias.DataFinalParaCalculo.Value.AddDays(-1);

            CalculoDiarias.DataHoraOntem = new DateTime(CalculoDiarias.DataHoraOntem.Year, CalculoDiarias.DataHoraOntem.Month, CalculoDiarias.DataHoraOntem.Day, 23, 59, 59);

            CalculoDiarias.Feriados = await SelecionarDatasFeriados(CalculoDiarias.Uf, CalculoDiarias.MunicipioId, CalculoDiarias.DataHoraGuarda.Date.Year, CalculoDiarias.DataHoraDiaria.Date.AddDays(10).Year);

            CalculoDiarias.QuantidadeDiariasPagas = await QuantidadeDiariasPagas(CalculoDiarias);

            CalculoDiarias.CobrarTodasDiarias = RegrasFaturamento?.Count > 0 && RegrasFaturamento.Any(w => w.FaturamentoRegraTipo.Codigo.Equals("CALCDIASNAOCOBRADAS"));

            if (CalculoDiarias.MaximoDiariasParaCobranca > 0 &&
                CalculoDiarias.QuantidadeDiariasPagas > 0 &&
                CalculoDiarias.CobrarTodasDiarias)
            {
                // Regra calcular diarias não cobradas para faturamento adicional - a partir da data da guarda
                CalculoDiarias.Diarias = DateTimeHelper.GetDaysBetweenTwoDates(CalculoDiarias.DataHoraGuarda.Date, CalculoDiarias.DataHoraDiaria.Date);

                CalculoDiarias.DataHoraInicialParaCalculo = CalculoDiarias.DataHoraGuarda;

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
                (CalculoDiarias.DataHoraGuarda.Date == CalculoDiarias.DataFinalParaCalculo.Value.Date))
            {
                CalculoDiarias.Diarias = 1;
            }
            // Ocorre quando só tem dias úteis
            else if (CalculoDiarias.Diarias == CalculoDiarias.QuantidadeDiasUteis)
            {
                if ((CalculoDiarias.DataFinalParaCalculo.Value.Hour > CalculoDiarias.DataHoraDiaria.Hour) ||
                   ((CalculoDiarias.DataFinalParaCalculo.Value.Hour == CalculoDiarias.DataHoraDiaria.Hour) &&
                   (CalculoDiarias.DataFinalParaCalculo.Value.Minute > CalculoDiarias.DataHoraDiaria.Minute)))
                {
                    CalculoDiarias.Diarias++;
                }
            }
            // Dias corridos, ignora dias não úteis e calcula a partir da Data da Guarda.
            else if (CalculoDiarias.IsCobrarDiariasDiasCorridos == "S")
            {
                if ((CalculoDiarias.DataFinalParaCalculo.Value.Hour > CalculoDiarias.DataHoraDiaria.Hour) ||
                   ((CalculoDiarias.DataFinalParaCalculo.Value.Hour == CalculoDiarias.DataHoraDiaria.Hour) &&
                   (CalculoDiarias.DataFinalParaCalculo.Value.Minute > CalculoDiarias.DataHoraDiaria.Minute)))
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
                CalculoDiarias.Diarias = AdicionarDiaRegraFaturamento(RegrasFaturamento, CalculoDiarias.DataFinalParaCalculo.Value, CalculoDiarias.Diarias);

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

                    CalculoDiarias.Diarias = AdicionarDiaRegraFaturamento(RegrasFaturamento, CalculoDiarias.DataFinalParaCalculo.Value, CalculoDiarias.Diarias);

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
                if ((CalculoDiarias.DataFinalParaCalculo.Value.Hour > CalculoDiarias.DataHoraDiaria.Hour) ||
                   ((CalculoDiarias.DataFinalParaCalculo.Value.Hour == CalculoDiarias.DataHoraDiaria.Hour) &&
                   (CalculoDiarias.DataFinalParaCalculo.Value.Minute > CalculoDiarias.DataHoraDiaria.Minute)))
                {
                    CalculoDiarias.Diarias++;
                }
            }

            int totalRealDias = CalculoDiarias.Diarias;

            CalculoDiarias.Diarias = RetornarMaximoDiaCobranca(CalculoDiarias.MaximoDiariasParaCobranca, CalculoDiarias.Diarias, CalculoDiarias.QuantidadeDiariasPagas);

            if (FaturamentoAdicional)
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

        private async Task<CalculoDiariasModel> SelecionarLocalizacaoDeposito(CalculoDiariasModel CalculoDiariasModel)
        {
            DepositoModel Deposito = await _context.Depositos
                .Include(i => i.CEP)
                .ThenInclude(t => t.Municipio)
                .Where(w => w.DepositoId.Equals(CalculoDiariasModel.DepositoId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito != null)
            {
                CalculoDiariasModel.Uf = Deposito.CEP.Municipio.Uf;

                CalculoDiariasModel.MunicipioId = Deposito.CEP.Municipio.MunicipioId;
            }

            return CalculoDiariasModel;
        }

        private async Task<int> QuantidadeDiariasPagas(CalculoDiariasModel CalculoDiarias)
        {
            if (CalculoDiarias.IsPrimeiroFaturamento == "N")
            {
                List<FaturamentoComposicaoModel> FaturamentoComposicoes = await _context.FaturamentoComposicoes
                    .Include(i => i.Faturamento)
                    .Where(w => w.TipoComposicao == "D" &&
                                w.QuantidadeComposicao > 0 &&
                                w.Faturamento.AtendimentoId == CalculoDiarias.AtendimentoId &&
                                w.Faturamento.Status == "P")
                    .ToListAsync();

                if (FaturamentoComposicoes?.Count > 0)
                {
                    return (int)FaturamentoComposicoes
                        .Sum(s => s.QuantidadeComposicao);
                }
            }

            return 0;
        }

        private async Task<List<DateTime>> SelecionarDatasFeriados(string uf, int MunicipioId, int AnoInicial, int AnoFinal = 0)
        {
            if (AnoFinal <= 0)
            {
                AnoFinal = AnoInicial;
            }

            List<DateTime> DatasFeriados = new();

            List<FeriadoModel> Feriados;

            for (int ano = AnoInicial; ano <= AnoFinal; ano++)
            {
                Feriados = await _context.Feriados
                    .Where(w => (w.Ano == null || w.Ano == ano) &&
                                ((w.FlagFeriadoNacional == "S") ||
                                 (w.Uf == uf && w.MunicipioId == null) ||
                                 (w.MunicipioId == MunicipioId)))
                    .ToListAsync();

                foreach (var item in Feriados)
                {
                    if (item.Ano == null)
                    {
                        DatasFeriados.Add(new(ano, item.Mes, item.Dia));
                    }
                    else
                    {
                        DatasFeriados.Add(new(item.Ano.Value, item.Mes, item.Dia));
                    }
                }
            }

            return DatasFeriados
                .Distinct()
                .OrderBy(o => o)
                .ToList();
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
            if (inputDate.DayOfWeek.Equals(DayOfWeek.Saturday) || inputDate.DayOfWeek.Equals(DayOfWeek.Sunday))
            {
                return false;
            }

            if (HolidayHelper.IsHoliday(inputDate))
            {
                return false;
            }

            if (Feriados?.Count > 0)
            {
                DateTime result = Feriados.Find(w => w.Date.Equals(inputDate.Date));

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

        private static int RegraNaoCobrarDiariaAtual(List<FaturamentoRegraModel> FaturamentoRegrasList, CalculoDiariasModel CalculoDiariasModel, List<DateTime> Feriados, int totalRealDias, int dias)
        {
            if (CalculoDiariasModel.IsComboio == "N" &&
                CalculoDiariasModel.IsPrimeiroFaturamento == "S" &&
                FaturamentoRegrasList?.Count > 0 &&
                dias > 1 &&
                FaturamentoRegrasList.Any(w => w.FaturamentoRegraTipo.Codigo.Equals("NAOCOBRARDIARIAATUAL")) &&
                IsDiaUtil(CalculoDiariasModel.DataFinalParaCalculo.Value, Feriados) &&
                CalculoDiariasModel.DataFinalParaCalculo.Value.Hour > CalculoDiariasModel.DataHoraDiaria.Hour &&
                (CalculoDiariasModel.MaximoDiariasParaCobranca > 0 && CalculoDiariasModel.MaximoDiariasParaCobranca >= totalRealDias))
            {
                dias--;
            }

            return dias;
        }

        private static int VerificarRegraNaoCobrarPrimeiraDiaria(List<FaturamentoRegraModel> FaturamentoRegras, CalculoDiariasModel CalculoDiariasModel, int dias)
        {
            if (CalculoDiariasModel.IsComboio == "N" &&
                CalculoDiariasModel.IsPrimeiroFaturamento == "S" &&
                FaturamentoRegras?.Count > 0
                && FaturamentoRegras.Any(w => w.FaturamentoRegraTipo.Codigo.Equals("NAOCOBRPRIMEIRDIARIA")))
            {
                dias--;
            }

            return dias;
        }
    }
}
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Services.Localizacao
{
    public class FeriadoService
    {
        private readonly AppDbContext _context;

        public FeriadoService(AppDbContext context)
        {
            _context = context;
        }

        public static int GetDiasUteis(DateTime dataInicial, DateTime dataFinal, List<DateTime> Feriados)
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

        public static bool IsDiaUtil(DateTime inputDate, List<DateTime> Feriados)
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

        public List<DateTime> ListDataFeriado(string UF, int MunicipioId, int AnoInicial, int AnoFinal = 0)
        {
            if (AnoFinal <= 0)
            {
                AnoFinal = AnoInicial;
            }

            List<DateTime> DatasFeriados = new();

            List<FeriadoModel> Feriados;

            for (int ano = AnoInicial; ano <= AnoFinal; ano++)
            {
                Feriados = _context.Feriado
                    .Where(x => (x.Ano == null || x.Ano == ano) &&
                                (x.FlagFeriadoNacional == "S" ||
                                 x.UF == UF && x.MunicipioId == null ||
                                 x.MunicipioId == MunicipioId))
                    .ToList();

                foreach (FeriadoModel item in Feriados)
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
                .OrderBy(x => x)
                .ToList();
        }
    }
}
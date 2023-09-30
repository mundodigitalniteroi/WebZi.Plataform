using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class FeriadoService
    {
        private readonly AppDbContext _context;

        public FeriadoService(AppDbContext context)
        {
            _context = context;
        }

        public List<DateTime> SelecionarDatasFeriados(string UF, int MunicipioId, int AnoInicial, int AnoFinal = 0)
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
                    .Where(w => (w.Ano == null || w.Ano == ano) &&
                                ((w.FlagFeriadoNacional == "S") ||
                                 (w.UF == UF && w.MunicipioId == null) ||
                                 (w.MunicipioId == MunicipioId)))
                    .ToList();

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
    }
}
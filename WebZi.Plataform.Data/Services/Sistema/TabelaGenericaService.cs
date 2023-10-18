using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Sistema;

namespace WebZi.Plataform.Data.Services.Sistema
{
    public class TabelaGenericaService
    {
        private readonly AppDbContext _context;

        public TabelaGenericaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TabelaGenericaModel>> List(string Codigo)
        {
            List<TabelaGenericaModel> result = await _context.TabelaGenerica
                .Where(x => x.Codigo == Codigo)
                .AsNoTracking()
                .ToListAsync();

            return result?.Count > 0 ? result
                .OrderBy(x => x.Valor1)
                .ToList() : null;
        }
    }
}
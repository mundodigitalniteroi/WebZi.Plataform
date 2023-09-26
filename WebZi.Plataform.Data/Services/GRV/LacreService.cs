using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Services.GRV;

namespace WebZi.Plataform.Data.Services.GRV
{
    public class LacreService
    {
        private readonly AppDbContext _context;

        public LacreService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LacreModel>> List(int GrvId, int UsuarioId)
        {
            if (!await new GrvService(_context).UserCanAccessGrv(GrvId, UsuarioId))
            {
                return null;
            }

            List<LacreModel> result = await _context.Lacres
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .ToListAsync();

            return result.OrderBy(o => o.Lacre).ToList();
        }
    }
}
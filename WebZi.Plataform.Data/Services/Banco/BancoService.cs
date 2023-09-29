using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Banco;

namespace WebZi.Plataform.Data.Services.Banco
{
    public class BancoService
    {
        private readonly AppDbContext _context;

        public BancoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BancoModel> GetById(short BancoId)
        {
            return await _context.Banco
                .Where(w => w.BancoId == BancoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<BancoModel> GetByName(string Name)
        {
            return await _context.Banco
                .Where(w => w.Nome == Name)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<BancoModel>> List()
        {
            List<BancoModel> result = await _context.Banco
                .AsNoTracking()
                .ToListAsync();

            return result.OrderBy(o => o.Nome).ToList();
        }
    }
}
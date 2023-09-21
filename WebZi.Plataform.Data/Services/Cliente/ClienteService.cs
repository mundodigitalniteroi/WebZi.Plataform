using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Cliente;

namespace WebZi.Plataform.Data.Services.Cliente
{
    public class ClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteModel>> List()
        {
            return await _context.Clientes
                .OrderBy(o => o.Nome)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ClienteModel> GetById(int id)
        {
            return await _context.Clientes
                .Where(w => w.ClienteId == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<ClienteModel> GetByName(string Name)
        {
            return await _context.Clientes
                .Where(w => w.Nome == Name)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
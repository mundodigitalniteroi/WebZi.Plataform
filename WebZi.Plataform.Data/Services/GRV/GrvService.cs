using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Services.GRV
{
    public class GrvService
    {
        private readonly AppDbContext _context;

        public GrvService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GrvModel> GetById(int id)
        {
            return await _context.Grvs
                .Include(i => i.StatusOperacao)
                .Where(w => w.GrvId.Equals(id))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<GrvModel> GetByProcesso(string numeroFormulario, int clienteId, int depositoId)
        {
            return await _context.Grvs
                .Include(i => i.StatusOperacao)
                .Where(w => w.NumeroFormularioGrv.Equals(numeroFormulario) && w.ClienteId.Equals(clienteId) && w.DepositoId.Equals(depositoId))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
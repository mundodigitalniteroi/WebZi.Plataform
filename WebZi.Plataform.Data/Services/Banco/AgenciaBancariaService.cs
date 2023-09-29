using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Banco;

namespace WebZi.Plataform.Data.Services.Banco
{
    public class AgenciaBancariaService
    {
        private readonly AppDbContext _context;

        public AgenciaBancariaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AgenciaBancariaModel> GetById(int AgenciaBancariaId)
        {
            return await _context.AgenciaBancaria
                .Where(w => w.BancoId == AgenciaBancariaId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<AgenciaBancariaModel> GetByAgencia(int BancoId, string Agencia)
        {
            return await _context.AgenciaBancaria
                .Where(w => w.BancoId == BancoId && w.CodigoAgencia == Agencia)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<BancoModel> List(int BancoId)
        {
            BancoModel result = await _context.Banco
                .Include(i => i.AgenciasBancarias.OrderBy(o => o.CodigoAgencia).ThenBy(t => t.ContaCorrente))
                .Where(w => w.BancoId == BancoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
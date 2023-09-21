using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Services.Localizacao
{
    public class CEPService
    {
        private readonly AppDbContext _context;

        public CEPService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CEPModel> GetById(int CEPId)
        {
            return await _context.CEPs
               .Include(i => i.Municipio)
               .Include(i => i.Municipio.Estado)
               .Include(i => i.Bairro)
               .Include(i => i.TipoLogradouro)
               .Where(w => w.CepId.Equals(CEPId))
               .AsNoTracking()
               .FirstOrDefaultAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Sistema;

namespace WebZi.Plataform.Data.Services.Sistema
{
    public class ConfiguracaoService
    {
        private readonly AppDbContext _context;

        public ConfiguracaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ConfiguracaoModel> Get()
        {
            return await _context.Configuracao
                .FirstOrDefaultAsync();
        }
    }
}
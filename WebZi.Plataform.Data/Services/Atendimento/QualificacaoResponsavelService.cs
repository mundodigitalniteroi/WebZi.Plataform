using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.Data.Services.Atendimento
{
    public class QualificacaoResponsavelService
    {
        private readonly AppDbContext _context;

        public QualificacaoResponsavelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QualificacaoResponsavelModel>> List()
        {
            var result = await _context.QualificacaoResponsavel
                .AsNoTracking()
                .ToListAsync();

            return result
                .OrderBy(o => o.Descricao)
                .ToList();
        }
    }
}
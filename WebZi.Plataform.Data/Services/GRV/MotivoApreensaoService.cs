using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV;

namespace WebZi.Plataform.Data.Services.GRV
{
    public class MotivoApreensaoService
    {
        private readonly AppDbContext _context;

        public MotivoApreensaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MotivoApreensaoViewModelList> List()
        {
            MotivoApreensaoViewModelList ResultView = new();

            List<MotivoApreensaoModel> result = await _context.MotivoApreensao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(o => o.Descricao)
                    .ToList();

                ResultView.MotivosApreensoes = result;

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
    }
}
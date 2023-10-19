using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV;

namespace WebZi.Plataform.Data.Services.GRV
{
    public class StatusOperacaoService
    {
        private readonly AppDbContext _context;

        public StatusOperacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StatusOperacaoViewModelList> GetById(string StatusOperacaoId)
        {
            StatusOperacaoViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(StatusOperacaoId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador do Status da Operação inválido");

                return ResultView;
            }

            StatusOperacaoModel result = await _context.StatusOperacao
                .Where(w => w.StatusOperacaoId == StatusOperacaoId.ToUpper().Trim())
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.ListagemStatusOperacao.Add(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<StatusOperacaoViewModelList> List()
        {
            StatusOperacaoViewModelList ResultView = new();

            List<StatusOperacaoModel> result = await _context.StatusOperacao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(o => o.Descricao)
                    .ToList();

                ResultView.ListagemStatusOperacao = result;

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
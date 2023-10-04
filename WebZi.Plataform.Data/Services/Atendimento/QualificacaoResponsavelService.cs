using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Atendimento;

namespace WebZi.Plataform.Data.Services.Atendimento
{
    public class QualificacaoResponsavelService
    {
        private readonly AppDbContext _context;

        public QualificacaoResponsavelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<QualificacaoResponsavelViewModelList> ListAsync()
        {
            QualificacaoResponsavelViewModelList ResultView = new()
            {
                QualificacoesResponsaveis = await _context.QualificacaoResponsavel
                .AsNoTracking()
                .ToListAsync()
            };

            if (ResultView.QualificacoesResponsaveis?.Count > 0)
            {
                ResultView.QualificacoesResponsaveis = ResultView.QualificacoesResponsaveis
                    .OrderBy(o => o.Descricao)
                    .ToList();

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(ResultView.QualificacoesResponsaveis.Count);

                return ResultView;
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                return ResultView;
            }
        }
    }
}
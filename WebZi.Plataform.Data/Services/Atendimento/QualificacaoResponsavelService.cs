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
            QualificacaoResponsavelViewModelList result = new()
            {
                QualificacoesResponsaveis = await _context.QualificacaoResponsavel
                .AsNoTracking()
                .ToListAsync()
            };

            if (result.QualificacoesResponsaveis?.Count > 0)
            {
                result.QualificacoesResponsaveis = result.QualificacoesResponsaveis
                    .OrderBy(o => o.Descricao)
                    .ToList();

                result.Mensagem = MensagemViewHelper.GetOkFound();

                return result;
            }
            else
            {
                result.Mensagem = MensagemViewHelper.GetNotFound();

                return result;
            }
        }
    }
}
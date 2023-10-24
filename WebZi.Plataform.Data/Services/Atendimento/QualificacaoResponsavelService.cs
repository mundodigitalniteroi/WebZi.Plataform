using AutoMapper;
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
        private readonly IMapper _mapper;

        public QualificacaoResponsavelService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<QualificacaoResponsavelViewModelList> ListAsync()
        {
            QualificacaoResponsavelViewModelList ResultView = new();

            List<QualificacaoResponsavelModel> result = await _context.QualificacaoResponsavel
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<QualificacaoResponsavelViewModel>>(result
                    .OrderBy(o => o.Descricao)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(ResultView.Listagem.Count);

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
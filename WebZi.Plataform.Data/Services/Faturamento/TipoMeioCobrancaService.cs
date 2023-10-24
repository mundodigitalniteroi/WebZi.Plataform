using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Faturamento;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class TipoMeioCobrancaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TipoMeioCobrancaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TipoMeioCobrancaViewModelList GetById(byte TipoMeioCobrancaId)
        {
            TipoMeioCobrancaViewModelList ResultView = new();

            TipoMeioCobrancaModel result = _context.TipoMeioCobranca
                .Where(w => w.TipoMeioCobrancaId == TipoMeioCobrancaId)
                .AsNoTracking()
                .FirstOrDefault();

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<TipoMeioCobrancaViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public TipoMeioCobrancaViewModelList List()
        {
            TipoMeioCobrancaViewModelList ResultView = new();

            List<TipoMeioCobrancaModel> result = _context.TipoMeioCobranca
                .Where(w => w.FlagAtivo == "S")
                .AsNoTracking()
                .ToList();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<TipoMeioCobrancaViewModel>>(result
                    .OrderBy(o => o.Descricao)
                    .ToList());

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
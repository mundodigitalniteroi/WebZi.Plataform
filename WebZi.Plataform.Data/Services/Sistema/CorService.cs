using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.ViewModel.Sistema;

namespace WebZi.Plataform.Data.Services.Sistema
{
    public class CorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CorViewModelList> List(string Cor)
        {
            List<CorModel> result = await _context.Cor
                .Where(w => !string.IsNullOrWhiteSpace(Cor) ? w.Cor.Contains(Cor.ToUpper().Trim()) : true)
                .AsNoTracking()
                .ToListAsync();

            CorViewModelList ResultView = new();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Cor não encontrada");

                return ResultView;
            }

            ResultView.Cores = _mapper.Map<List<CorViewModel>>(result.OrderBy(x => x.Cor).ToList());

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.ViewModel.Veiculo;

namespace WebZi.Plataform.Data.Services.GGV
{
    public class TipoAvariaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TipoAvariaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TipoAvariaViewModelList> ListarTipoAvaria()
        {
            TipoAvariaViewModelList ResultView = new();

            List<TipoAvariaModel> result = await _context.TipoAvaria
                .AsNoTracking()
                .ToListAsync();

            ResultView.TiposAvarias = _mapper.Map<List<TipoAvariaViewModel>>(result.OrderBy(x => x.Descricao).ToList());

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }
    }
}
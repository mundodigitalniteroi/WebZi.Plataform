using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.ViewModel.Veiculo;

namespace WebZi.Plataform.Data.Services.Veiculo
{
    public class TipoVeiculoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TipoVeiculoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TipoVeiculoViewModelList> List()
        {
            TipoVeiculoViewModelList ResultView = new();

            List<TipoVeiculoModel> result = await _context.TipoVeiculo
                .AsNoTracking()
                .ToListAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Marca/Modelo não encontrado");

                return ResultView;
            }

            ResultView.TiposVeiculos = _mapper.Map<List<TipoVeiculoViewModel>>(result.OrderBy(x => x.Descricao).ToList());

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }
    }
}
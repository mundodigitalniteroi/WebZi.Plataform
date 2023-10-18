using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.ViewModel.Veiculo;

namespace WebZi.Plataform.Data.Services.Veiculo
{
    public class MarcaModeloService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MarcaModeloService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MarcaModeloViewModelList> List(string MarcaModelo)
        {
            MarcaModeloViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(MarcaModelo))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Informe a descrição da Marca/Modelo");

                return ResultView;
            }

            List<MarcaModeloModel> result = await _context.MarcaModelo
                .Where(w => w.MarcaModelo.Contains(MarcaModelo.ToUpper().Trim()))
                .OrderBy(x => x.MarcaModelo)
                .Take(100)
                .AsNoTracking()
                .ToListAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Marca/Modelo não encontrado");

                return ResultView;
            }

            ResultView.MarcasModelos = _mapper.Map<List<MarcaModeloViewModel>>(result);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }
    }
}
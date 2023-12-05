using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.Sistema;

namespace WebZi.Plataform.Data.Services.Sistema
{
    public class SistemaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SistemaService(AppDbContext context)
        {
            _context = context;
        }

        public SistemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CorViewModelList> ListarCorAsync(string Cor = "")
        {
            List<CorModel> result = await _context.Cor
                .Where(w => !string.IsNullOrWhiteSpace(Cor) ? w.Cor.Contains(Cor.ToUpper().Trim()) : true)
                .AsNoTracking()
                .ToListAsync();

            CorViewModelList ResultView = new();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper
                    .Map<List<CorViewModel>>(result.OrderBy(x => x.Cor)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Cor não encontrada");
            }

            return ResultView;
        }

        public async Task<ConfiguracaoModel> GetConfiguracaoSistemaAsync()
        {
            return await _context.Configuracao
                .FirstOrDefaultAsync();
        }
    }
}
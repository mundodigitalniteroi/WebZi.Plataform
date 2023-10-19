using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Sistema;
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

        public async Task<CorViewModelList> ListarCores(string Cor = "")
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

            ResultView.ListagemCor = _mapper
                .Map<List<CorViewModel>>(result.OrderBy(x => x.Cor)
                .ToList());

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        /// <summary>
        /// Retorna registros da Tabela Genérica.
        /// Por padrão, a ordenação é realizada na propriedade Valor1.
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public async Task<List<TabelaGenericaModel>> ListarTabelaGenerica(string Codigo)
        {
            List<TabelaGenericaModel> result = await _context.TabelaGenerica
                .Where(x => x.Codigo == Codigo)
                .AsNoTracking()
                .ToListAsync();

            return result?.Count > 0 ? result
                .OrderBy(x => x.Valor1)
                .ToList() : null;
        }

        public async Task<ConfiguracaoModel> SelecionarConfiguracaoSistema()
        {
            return await _context.Configuracao
                .FirstOrDefaultAsync();
        }
    }
}
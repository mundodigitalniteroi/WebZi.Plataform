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

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Cor não encontrada");
            }

            return ResultView;
        }

        /// <summary>
        /// Retorna registros da Tabela Genérica.
        /// Por padrão, a ordenação é realizada na propriedade Sequencia seguido de Valor1.
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public async Task<List<TabelaGenericaModel>> ListarTabelaGenericaAsync(string Codigo)
        {
            List<TabelaGenericaModel> result = await _context.TabelaGenerica
                .Where(x => x.Codigo == Codigo)
                .AsNoTracking()
                .ToListAsync();

            return result?.Count > 0 ? result
                .OrderBy(x => x.Sequencia)
                .ThenBy(x => x.Descricao)
                .ToList() : null;
        }

        public TabelaGenericaModel GetById(string Codigo, int TabelaGenericaId)
        {
            return _context.TabelaGenerica
                .Where(x => x.Codigo == Codigo
                         && x.TabelaGenericaId == TabelaGenericaId)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public async Task<TabelaGenericaModel> GetByIdAsync(string Codigo, int TabelaGenericaId)
        {
            return await _context.TabelaGenerica
                .Where(x => x.Codigo == Codigo
                         && x.TabelaGenericaId == TabelaGenericaId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<TabelaGenericaModel>> ListarTabelaGenericaByIdAsync(string Codigo, int TabelaGenericaId)
        {
            List<TabelaGenericaModel> result = await _context.TabelaGenerica
                .Where(x => x.Codigo == Codigo
                         && x.TabelaGenericaId == TabelaGenericaId)
                .AsNoTracking()
                .ToListAsync();

            return result?.Count > 0 ? result
                .OrderBy(x => x.Sequencia)
                .ThenBy(x => x.Descricao)
                .ToList() : null;
        }

        public async Task<TabelaGenericaViewModelList> ListarTabelaGenericaViewModelAsync(string Codigo)
        {
            TabelaGenericaViewModelList ResultView = new();

            List<TabelaGenericaModel> result = await ListarTabelaGenericaAsync(Codigo);

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                return ResultView;
            }

            ResultView.Listagem = _mapper
                .Map<List<TabelaGenericaViewModel>>(result);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<ConfiguracaoModel> SelecionarConfiguracaoSistemaAsync()
        {
            return await _context.Configuracao
                .FirstOrDefaultAsync();
        }
    }
}
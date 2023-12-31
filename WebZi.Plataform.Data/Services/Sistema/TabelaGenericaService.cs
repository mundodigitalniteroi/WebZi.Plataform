using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.Models.Sistema;

namespace WebZi.Plataform.Data.Services.Sistema
{
    public class TabelaGenericaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TabelaGenericaService(AppDbContext context)
        {
            _context = context;
        }

        public TabelaGenericaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TabelaGenericaModel GetById(int TabelaGenericaId)
        {
            return _context.TabelaGenerica
                .AsNoTracking()
                .FirstOrDefault(x => x.TabelaGenericaId == TabelaGenericaId);
        }

        public async Task<TabelaGenericaModel> GetByIdAsync(int TabelaGenericaId)
        {
            return await _context.TabelaGenerica
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TabelaGenericaId == TabelaGenericaId);
        }

        public async Task<string> GetValorCadastroAsync(int TabelaGenericaId)
        {
            TabelaGenericaModel result = await _context.TabelaGenerica
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TabelaGenericaId == TabelaGenericaId);

            return result != null ? result.ValorCadastro : string.Empty;
        }

        /// <summary>
        /// Retorna registros da Tabela Genérica.
        /// Por padrão, a ordenação é realizada na propriedade Sequencia seguido de Valor1.
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public async Task<List<TabelaGenericaModel>> ListAsync(string Codigo)
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

        public async Task<TabelaGenericaListDTO> ListToViewModelAsync(string Codigo)
        {
            TabelaGenericaListDTO ResultView = new();

            List<TabelaGenericaModel> result = await ListAsync(Codigo);

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();

                return ResultView;
            }

            ResultView.Listagem = _mapper
                .Map<List<TabelaGenericaDTO>>(result);

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }
    }
}
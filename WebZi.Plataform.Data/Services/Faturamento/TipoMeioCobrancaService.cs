using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.Models.Banco;

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

        public async Task<TipoMeioCobrancaListDTO> GetByIdAsync(byte TipoMeioCobrancaId)
        {
            TipoMeioCobrancaListDTO ResultView = new();

            TipoMeioCobrancaModel result = await _context.TipoMeioCobranca
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoMeioCobrancaId == TipoMeioCobrancaId);

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<TipoMeioCobrancaDTO>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<TipoMeioCobrancaListDTO> ListAsync()
        {
            TipoMeioCobrancaListDTO ResultView = new();

            List<TipoMeioCobrancaModel> result = await _context.TipoMeioCobranca
                .Where(x => x.FlagAtivo == "S")
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<TipoMeioCobrancaDTO>>(result
                    .OrderBy(x => x.Descricao)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }
    }
}
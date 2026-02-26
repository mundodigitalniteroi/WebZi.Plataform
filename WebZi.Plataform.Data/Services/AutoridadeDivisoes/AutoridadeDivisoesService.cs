using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.GRV;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Services.AutoridadeDivisoes
{
    public class AutoridadeDivisoesService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AutoridadeDivisoesService(AppDbContext context)
        {
            _context = context;
        }
        public AutoridadeDivisoesService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AutoridadesDivisoesListDTO> ListAutoridadeDivisoesAsync()
        {

            AutoridadesDivisoesListDTO ResultView = new();
            #region Consulta
                List<AutoridadeDivisaoModel> results = await _context.AutoridadesDivisoes.AsNoTracking().ToListAsync();
            #endregion

            if (results?.Count() > 0)
            {
                ResultView.Listagem = _mapper.Map<List<AutoridadesDivisoesDTO>>(results).ToList();

                ResultView.Mensagem = MensagemViewHelper.SetFound(results.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }
            return ResultView;
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel.GRV;

namespace WebZi.Plataform.Data.Services.GRV
{
    public class LacreService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LacreService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LacreResultViewModelList> List(int GrvId, int UsuarioId)
        {
            LacreResultViewModelList LacreResultView = new();

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                LacreResultView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário desativado ou inexistente");

                return LacreResultView;
            }
            else if (!await new GrvService(_context).UserCanAccessGrv(GrvId, UsuarioId))
            {
                LacreResultView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV");

                return LacreResultView;
            }

            List<LacreModel> result = await _context.Lacre
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .ToListAsync();

            if (result.Count > 0)
            {
                result = result
                    .OrderBy(o => o.Lacre)
                    .ToList();

                LacreResultView.Mensagem = MensagemViewHelper.GetOk("Registro encontrado");

                LacreResultView.Lacres = _mapper.Map<List<LacreResultViewModel>>(result);
            }
            else
            {
                LacreResultView.Mensagem = MensagemViewHelper.GetNotFound("Lacres sem permissão de acesso ou inexistente");
            }

            return LacreResultView;
        }
    }
}
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

        public async Task<LacreViewModelList> List(int GrvId, int UsuarioId)
        {
            List<string> erros = new();

            if (GrvId <= 0)
            {
                erros.Add("Identificador do GRV inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do GRV inválido");
            }

            LacreViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized();

                return ResultView;
            }

            GrvModel Grv = _context.Grv
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefault();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("GRV não encontrado");

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV");

                return ResultView;
            }

            List<LacreModel> result = await _context.Lacre
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Lacres = _mapper.Map<List<LacreViewModel>>(result
                    .OrderBy(o => o.Lacre)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
    }
}
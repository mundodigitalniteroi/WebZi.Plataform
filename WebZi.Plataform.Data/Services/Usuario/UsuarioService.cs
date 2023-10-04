using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.ViewModel.Usuario;

namespace WebZi.Plataform.Domain.Services.Usuario
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public UsuarioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsuarioViewModel> GetById(int UsuarioId)
        {
            UsuarioViewModel ResultView = new();

            UsuarioModel result = await _context.Usuario
                .Where(w => w.UsuarioId == UsuarioId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView = _mapper.Map<UsuarioViewModel>(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();

                return ResultView;
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                return ResultView;
            }
        }

        public async Task<UsuarioViewModel> GetByLogin(string Login)
        {
            UsuarioViewModel ResultView = new();

            UsuarioModel result = await _context.Usuario
                .Where(w => w.Login.Contains(Login.ToUpper().Trim()))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView = _mapper.Map<UsuarioViewModel>(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();

                return ResultView;
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                return ResultView;
            }
        }

        public bool IsUserActive(int UsuarioId)
        {
            UsuarioModel Usuario = _context.Usuario
                .Where(w => w.UsuarioId == UsuarioId)
                .AsNoTracking()
                .FirstOrDefault();

            return Usuario != null && Usuario.FlagAtivo != "N";
        }
    }
}
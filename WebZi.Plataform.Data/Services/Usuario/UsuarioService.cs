using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
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

        public UsuarioViewModel GetById(int UsuarioId)
        {
            UsuarioModel Usuario = _context.Usuario
                .Where(w => w.UsuarioId == UsuarioId)
                .AsNoTracking()
                .FirstOrDefault();

            if (Usuario == null)
            {
                return null;
            }

            return _mapper.Map<UsuarioViewModel>(Usuario);
        }

        public UsuarioViewModel GetByLogin(string Login)
        {
            UsuarioModel Usuario = _context.Usuario
                .Where(w => w.Login == Login)
                .AsNoTracking()
                .FirstOrDefault();

            if (Usuario == null)
            {
                return null;
            }

            return _mapper.Map<UsuarioViewModel>(Usuario);
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
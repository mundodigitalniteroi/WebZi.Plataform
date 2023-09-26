using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Usuario.ViewModel;

namespace WebZi.Plataform.Domain.Services.Usuario
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsuarioViewModel> GetById(int UsuarioId)
        {
            UsuarioModel Usuario = await _context.Usuarios
                .Where(w => w.UsuarioId == UsuarioId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Usuario == null)
            {
                return null;
            }

            return _mapper.Map<UsuarioViewModel>(Usuario);
        }

        public async Task<UsuarioViewModel> GetByLogin(string Login)
        {
            UsuarioModel Usuario = await _context.Usuarios
                .Where(w => w.Login == Login)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Usuario == null)
            {
                return null;
            }

            return _mapper.Map<UsuarioViewModel>(Usuario);
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.GRV.ViewModel;
using WebZi.Plataform.Domain.Models.Usuario.View;

namespace WebZi.Plataform.Domain.Services.GRV
{
    public class GrvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GrvService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GrvViewModel> GetById(int GrvId, int UsuarioId)
        {
            GrvModel Grv = await _context.Grvs
                .Where(w => w.GrvId.Equals(GrvId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return null;
            }
            else
            {
                ViewUsuarioClienteDepositoModel Usuario = await _context.ViewUsuariosClientesDepositos
                    .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                    .FirstOrDefaultAsync();

                if (Usuario == null)
                {
                    return null;
                }
            }

            return _mapper.Map<GrvViewModel>(Grv);
        }

        public async Task<GrvViewModel> GetByNumeroFormularioGrv(string NumeroFormularioGrv, int ClienteId, int DepositoId, int UsuarioId)
        {
            GrvModel Grv = await _context.Grvs
                .Where(w => w.NumeroFormularioGrv.Equals(NumeroFormularioGrv) && w.ClienteId.Equals(ClienteId) && w.DepositoId.Equals(DepositoId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return null;
            }
            else
            {
                ViewUsuarioClienteDepositoModel Usuario = await _context.ViewUsuariosClientesDepositos
                    .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                    .FirstOrDefaultAsync();

                if (Usuario == null)
                {
                    return null;
                }
            }

            return _mapper.Map<GrvViewModel>(Grv);
        }

        public async Task<bool> GrvExists(int GrvId)
        {
            GrvModel Grv = await _context.Grvs
                .Where(w => w.GrvId.Equals(GrvId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return Grv != null;
        }

        public async Task<bool> UserCanAccessGrv(int GrvId, int UsuarioId)
        {
            GrvModel Grv = await _context.Grvs
                .Where(w => w.GrvId.Equals(GrvId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return false;
            }
            
            ViewUsuarioClienteDepositoModel Usuario = await _context.ViewUsuariosClientesDepositos
                .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                .FirstOrDefaultAsync();

            return Usuario != null;
        }

        public async Task<bool> UserCanAccessGrv(GrvModel Grv, int UsuarioId)
        {
            ViewUsuarioClienteDepositoModel Usuario = await _context.ViewUsuariosClientesDepositos
                .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                .FirstOrDefaultAsync();

            return Usuario != null;
        }
    }
}
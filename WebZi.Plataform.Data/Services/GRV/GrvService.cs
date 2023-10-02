using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Domain.Services.GRV
{
    public class GrvService
    {
        private readonly AppDbContext _context;

        public GrvService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GrvModel> GetById(int GrvId, int UsuarioId)
        {
            GrvModel Grv = await _context.Grv
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return null;
            }
            else
            {
                ViewUsuarioClienteDepositoModel Usuario = await _context.ViewUsuarioClienteDeposito
                    .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                    .FirstOrDefaultAsync();

                if (Usuario == null)
                {
                    return null;
                }
            }

            return Grv;
        }

        public async Task<GrvModel> GetByNumeroFormularioGrv(string NumeroFormularioGrv, int ClienteId, int DepositoId, int UsuarioId)
        {
            GrvModel Grv = await _context.Grv
                .Where(w => w.NumeroFormularioGrv.Equals(NumeroFormularioGrv) && w.ClienteId.Equals(ClienteId) && w.DepositoId.Equals(DepositoId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return null;
            }
            else
            {
                ViewUsuarioClienteDepositoModel Usuario = await _context.ViewUsuarioClienteDeposito
                    .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                    .FirstOrDefaultAsync();

                if (Usuario == null)
                {
                    return null;
                }
            }

            return Grv;
        }

        public async Task<bool> GrvExists(int GrvId)
        {
            GrvModel Grv = await _context.Grv
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return Grv != null;
        }

        public async Task<bool> UserCanAccessGrv(int GrvId, int UsuarioId)
        {
            GrvModel Grv = await _context.Grv
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return false;
            }
            
            ViewUsuarioClienteDepositoModel Usuario = await _context.ViewUsuarioClienteDeposito
                .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                .FirstOrDefaultAsync();

            return Usuario != null;
        }

        public async Task<bool> UserCanAccessGrv(GrvModel Grv, int UsuarioId)
        {
            ViewUsuarioClienteDepositoModel Usuario = await _context.ViewUsuarioClienteDeposito
                .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                .FirstOrDefaultAsync();

            return Usuario != null;
        }
    }
}
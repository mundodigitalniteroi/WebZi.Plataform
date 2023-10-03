using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.Views.Usuario;

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

        public async Task<GrvViewModelList> GetById(int GrvId, int UsuarioId)
        {
            List<string> erros = new();

            if (GrvId <= 0)
            {
                erros.Add("Identificador do GrvId inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
            }

            GrvViewModelList ResultView = new();

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

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

            ResultView.Grvs = _mapper.Map<List<GrvViewModel>>(Grv);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<GrvViewModelList> GetByNumeroFormularioGrv(string NumeroFormularioGrv, int ClienteId, int DepositoId, int UsuarioId)
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(NumeroFormularioGrv))
            {
                erros.Add("Informe o Número do Processo");
            }

            if (ClienteId <= 0)
            {
                erros.Add("Identificador do Cliente inválido");
            }

            if (DepositoId <= 0)
            {
                erros.Add("Identificador do Depósito inválido ");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
            }

            GrvViewModelList ResultView = new();

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);
                
                return ResultView;
            }

            ClienteModel Cliente = await _context.Cliente
                .Where(w => w.ClienteId == ClienteId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Cliente inexistente");

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Depósito inexistente");

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.NumeroFormularioGrv.Equals(NumeroFormularioGrv) && w.ClienteId.Equals(ClienteId) && w.DepositoId.Equals(DepositoId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

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

            ResultView.Grvs = _mapper.Map<List<GrvViewModel>>(Grv);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public bool GrvExists(int GrvId)
        {
            return _context.Grv
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefault() != null;
        }

        public bool UserCanAccessGrv(int ClienteId, int DepositoId, int UsuarioId)
        {
            ViewUsuarioClienteDepositoModel Usuario = _context.ViewUsuarioClienteDeposito
                .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == ClienteId && w.DepositoId == DepositoId)
                .FirstOrDefault();

            return Usuario != null;
        }

        public bool UserCanAccessGrv(GrvModel Grv, int UsuarioId)
        {
            if (Grv == null)
            {
                return false;
            }

            return _context.ViewUsuarioClienteDeposito
                .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                .FirstOrDefault() != null;
        }
    }
}
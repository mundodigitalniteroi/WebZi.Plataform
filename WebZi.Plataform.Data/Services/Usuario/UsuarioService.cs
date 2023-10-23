using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.ViewModel.Cliente;
using WebZi.Plataform.Domain.ViewModel.Deposito;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.ViewModel.Usuario;
using WebZi.Plataform.Domain.Views.Usuario;

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

        private async Task<UsuarioViewModel> Get(int UsuarioId, string Login, string Password = "")
        {
            UsuarioViewModel ResultView = new();

            Login = Login.ToUpper().Trim();

            Password = Password.ToUpper().Trim();

            if (UsuarioId <= 0 && string.IsNullOrWhiteSpace(Login))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Informe o Identificador do Usuário ou o Login");

                return ResultView;
            }
            else if (string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Ao informar a Senha do Usuário, é preciso informar o Login");

                return ResultView;
            }

            if (!string.IsNullOrWhiteSpace(Password))
            {
                StringBuilder SQL = new();

                SQL.AppendLine("SELECT id_usuario AS Value");

                SQL.AppendLine("  FROM dbo.tb_dep_usuarios");

                SQL.AppendLine(" WHERE 1 = 1");

                SQL.Append("   AND login = @login");

                SQL.Append("   AND senha1 = HASHBYTES('MD5', @senha)");

                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("@login", SqlDbType.VarChar)
                    {
                        Value = Login
                    },

                    new SqlParameter("@senha", SqlDbType.VarChar)
                    {
                        Value = Password
                    }
                };

                int? Id = _context.Database.SqlQueryRaw<int>(SQL.ToString(), sqlParameter)
                    .FirstOrDefault();

                if (Id != null && Id >= 1)
                {
                    UsuarioId = Id.Value;
                }
                else
                {
                    ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoUsuario);

                    return ResultView;
                }
            }

            UsuarioModel result = await _context.Usuario
                .Where(x => (UsuarioId > 0 ? x.UsuarioId == UsuarioId : true) &&
                             !string.IsNullOrWhiteSpace(Login) ? x.Login == Login : true)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView = _mapper.Map<UsuarioViewModel>(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();

                var ListagemUsuarioClienteDeposito = await _context.ViewUsuarioClienteDeposito
                    .Where(x => x.UsuarioId == UsuarioId)
                    .Select(x => new { x.ClienteId, x.DepositoId })
                    .AsNoTracking()
                    .ToListAsync();

                if (ListagemUsuarioClienteDeposito?.Count > 0)
                {
                    ListagemUsuarioClienteDeposito = ListagemUsuarioClienteDeposito
                        .OrderBy(x => x.ClienteId)
                        .ThenBy(x => x.DepositoId)
                        .ToList();

                    foreach (var item in ListagemUsuarioClienteDeposito)
                    {
                        ResultView.ListagemClienteDepositoAssociado.Add(new UsuarioClienteDepositoViewModel { ClienteId = item.ClienteId, DepositoId = item.DepositoId });
                    }
                }
                else
                {
                    ResultView.Mensagem.AvisosInformativos.Add("Atenção. Este Usuário não possui associação com Cliente e Depósito");
                }

                return ResultView;
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoUsuario);

                return ResultView;
            }
        }

        public async Task<UsuarioViewModel> GetById(int UsuarioId)
        {
            return await Get(UsuarioId, string.Empty);
        }

        public async Task<UsuarioViewModel> GetByLogin(string Login, string Password = "")
        {
            return await Get(0, Login, Password);
        }

        public bool IsUserActive(int UsuarioId)
        {
            UsuarioModel Usuario = _context.Usuario
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .FirstOrDefault();

            return Usuario != null && Usuario.FlagAtivo != "N";
        }

        public async Task<UsuarioClienteDepositoReboqueViewModelList> ListarUsuarioClienteDepositoReboque(int UsuarioId)
        {
            UsuarioClienteDepositoReboqueViewModelList ResultView = new();

            List<ViewUsuarioClienteDepositoReboqueModel> result = await _context.ViewUsuarioClienteDepositoReboque
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.ListagemUsuarioClienteDepositoReboque = _mapper.Map<List<UsuarioClienteDepositoReboqueViewModel>>(result.OrderBy(o => o.ReboquePlaca).ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
        
        public async Task<UsuarioClienteDepositoReboquistaViewModelList> ListarUsuarioClienteDepositoReboquista(int UsuarioId)
        {
            UsuarioClienteDepositoReboquistaViewModelList ResultView = new();

            List<ViewUsuarioClienteDepositoReboquistaModel> result = await _context.ViewUsuarioClienteDepositoReboquista
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.ListagemUsuarioClienteDepositoReboquista = _mapper.Map<List<UsuarioClienteDepositoReboquistaViewModel>>(result.OrderBy(o => o.ReboquistaNome).ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboqueSimplificadoViewModelList> ListarUsuarioClienteDepositoReboqueSimplificada(int UsuarioId)
        {
            ReboqueSimplificadoViewModelList ResultView = new();

            UsuarioClienteDepositoReboqueViewModelList result = await ListarUsuarioClienteDepositoReboque(UsuarioId);

            if (result.ListagemUsuarioClienteDepositoReboque?.Count > 0)
            {
                ResultView.ListagemReboque = _mapper.Map<List<ReboqueSimplificadoViewModel>>(result.ListagemUsuarioClienteDepositoReboque);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.ListagemUsuarioClienteDepositoReboque.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboquistaSimplificadoViewModelList> ListarUsuarioClienteDepositoReboquistaSimplificada(int UsuarioId)
        {
            ReboquistaSimplificadoViewModelList ResultView = new();

            UsuarioClienteDepositoReboquistaViewModelList result = await ListarUsuarioClienteDepositoReboquista(UsuarioId);

            if (result.ListagemUsuarioClienteDepositoReboquista?.Count > 0)
            {
                ResultView.ListagemReboquista = _mapper.Map<List<ReboquistaSimplificadoViewModel>>(result.ListagemUsuarioClienteDepositoReboquista);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.ListagemUsuarioClienteDepositoReboquista.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
    }
}
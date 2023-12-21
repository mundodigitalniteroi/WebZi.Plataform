using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Enums;
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

        private async Task<UsuarioViewModel> GetAsync(int UsuarioId, string Username, string Password)
        {
            UsuarioViewModel ResultView = new();

            Username = Username.ToUpper().Trim();

            Password = Password.ToUpper().Trim();

            if (UsuarioId <= 0 && string.IsNullOrWhiteSpace(Username))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Informe o Identificador do Usuário ou o Login");

                return ResultView;
            }
            else if (string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Ao informar a Senha do Usuário, é preciso informar o Login");

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
                        Value = Username
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
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoUsuario);

                    return ResultView;
                }
            }

            UsuarioModel result = await _context.Usuario
                .Where(x => (UsuarioId > 0 ? x.UsuarioId == UsuarioId : true) &&
                             !string.IsNullOrWhiteSpace(Username) ? x.Login == Username : true)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView = new();

                ResultView = _mapper.Map<UsuarioViewModel>(result);

                ResultView.Mensagem = MensagemViewHelper.SetFound();

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
                        ResultView.ListagemClienteDepositoAssociado.Add(new UsuarioClienteDepositoViewModel { IdentificadorCliente = item.ClienteId, IdentificadorDeposito = item.DepositoId });
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
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoUsuario);

                return ResultView;
            }
        }

        public async Task<UsuarioViewModel> GetByIdAsync(int UsuarioId)
        {
            return await GetAsync(UsuarioId, string.Empty, string.Empty);
        }

        public async Task<UsuarioViewModel> GetByUsernameAsync(string Username)
        {
            return await GetAsync(0, Username, string.Empty);
        }

        public async Task<UsuarioViewModel> GetByLoginAsync(string Username, string Password)
        {
            return await GetAsync(0, Username, Password);
        }

        public bool IsUserActive(int UsuarioId)
        {
            UsuarioModel Usuario = _context.Usuario
                .AsNoTracking()
                .FirstOrDefault(x => x.UsuarioId == UsuarioId);

            return Usuario != null && Usuario.FlagAtivo != "N";
        }

        public async Task<bool> IsUserActiveAsync(int UsuarioId)
        {
            UsuarioModel Usuario = await _context.Usuario
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UsuarioId == UsuarioId);

            return Usuario != null && Usuario.FlagAtivo != "N";
        }
    }
}
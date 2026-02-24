using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Services.Usuario
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public UsuarioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UsuarioService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        private async Task<UsuarioDTO> GetAsync(int UsuarioId, string Login, string Password)
        {
            UsuarioDTO ResultView = new();

            Login = Login.ToUpper().Trim();

            Password = Password.ToUpper().Trim();

            if (UsuarioId <= 0 && string.IsNullOrWhiteSpace(Login))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Informe o Identificador do Usuário ou o Login");

                return ResultView;
            }
            else if (string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password))
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
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound("Usuário ou senha inválidos");

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
                ResultView = new();

                ResultView = _mapper.Map<UsuarioDTO>(result);

                ResultView.Mensagem = MensagemViewHelper.SetFound();

                //var ListagemUsuarioClienteDeposito = await _context.ViewUsuarioClienteDeposito
                //    .Where(x => x.UsuarioId == UsuarioId)
                //    .Select(x => new { x.ClienteId, x.DepositoId })
                //    .AsNoTracking()
                //    .ToListAsync();

                //if (ListagemUsuarioClienteDeposito?.Count > 0)
                //{
                //    ListagemUsuarioClienteDeposito = ListagemUsuarioClienteDeposito
                //        .OrderBy(x => x.ClienteId)
                //        .ThenBy(x => x.DepositoId)
                //        .ToList();

                //    foreach (var item in ListagemUsuarioClienteDeposito)
                //    {
                //        ResultView.ListagemClienteDepositoAssociado.Add(new UsuarioClienteDepositoDTO { IdentificadorCliente = item.ClienteId, IdentificadorDeposito = item.DepositoId });
                //    }
                //}
                //else
                //{
                //    ResultView.Mensagem.AvisosInformativos.Add("Atenção. Este Usuário não possui associação com Cliente e Depósito");
                //}

                return ResultView;
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoUsuario);

                return ResultView;
            }
        }

        public async Task<UsuarioPorNomeOuLoginListDTO> GetByUsernameOrLogin(string login, string username)
        {
            UsuarioPorNomeOuLoginListDTO result = new();

            if (string.IsNullOrWhiteSpace(login) && string.IsNullOrWhiteSpace(username))
            {
                result.Mensagem = MensagemViewHelper
                    .SetBadRequest("Informe o Login ou o Username");
                return result;
            }

            login = login?.ToUpper().Trim();
            username = username?.ToUpper().Trim();


            var usuarios = await _context.Usuario
                .Include(p => p.Pessoa)
                .AsNoTracking()
                .Where(x => 
                      (!string.IsNullOrWhiteSpace(login) && x.Login == login) ||
                      (!string.IsNullOrWhiteSpace(username) && x.Pessoa.Nome == username))
                .ToListAsync();

            if(usuarios is null || usuarios.Count <= 0)
            {
                result.Mensagem = MensagemViewHelper.SetNotFound("Nenhum usuário encontrado");
                return result;
            }
            result.Listagem = _mapper.Map<List<UsuarioPorNomeOuLoginDTO>>(usuarios);
            result.Mensagem = MensagemViewHelper.SetFound(usuarios.Count);
            return result;
        }
        public async Task<UsuarioDTO> GetByIdAsync(int UsuarioId)
        {
            return await GetAsync(UsuarioId, string.Empty, string.Empty);
        }

        public async Task<UsuarioDTO> GetByUsernameAsync(string Username)
        {
            return await GetAsync(0, Username, string.Empty);
        }

        public async Task<UsuarioDTO> GetByCredentialsAsync(string Username, string Password)
        {
            var result = await GetAsync(0, Username, Password);

            if (result.Mensagem.HtmlStatusCode == HtmlStatusCodeEnum.Ok)
            {
                result.Token = GenerateJwtToken(result, Username);
            }

            return result;
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

        public async Task<bool> IsUserAssociadoClienteDepositoAsync(int UsuarioId, int ClienteId, int DepositoId)
        {
            return await _context.ViewUsuarioClienteDeposito
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UsuarioId == UsuarioId
                                       && x.ClienteId == ClienteId
                                       && x.DepositoId == DepositoId) != null;
        }

        private string GenerateJwtToken(UsuarioDTO usuario, string username)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var secret = jwtSection["Secret"];
            int.TryParse(jwtSection["ExpirationMinutes"], out int expirationMinutes);
            if (expirationMinutes <= 0) expirationMinutes = 60;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.IdentificadorUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username ?? usuario.Login ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret ?? string.Empty));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(expirationMinutes);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expires,
                signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
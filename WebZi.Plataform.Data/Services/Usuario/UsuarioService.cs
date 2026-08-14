using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Pessoa;
using WebZi.Plataform.Domain.DTO.Cliente;
using WebZi.Plataform.Domain.DTO.Deposito;
using WebZi.Plataform.Domain.DTO.Pessoa;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Pessoa.Contato;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Options;
using WebZi.Plataform.Domain.ViewModel.Usuario;
using WebZi.Plataform.Domain.ViewModel.Usuario.CadastroUsuario;
using WebZi.Plataform.Domain.ViewModel.Usuario.AtualizarUsuario;
using WebZi.Plataform.Domain.ViewModel.Usuario.CadastrarSenha;

namespace WebZi.Plataform.Data.Services.Usuario
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _provider;
        private readonly IOptions<JwtOptions> _options;


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

        public UsuarioService(AppDbContext context, IMapper mapper, IConfiguration configuration,
            IOptions<JwtOptions> options)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _options = options;
        }

        public UsuarioService(AppDbContext context, IMapper mapper, IConfiguration configuration,
            IOptions<JwtOptions> options, IServiceProvider provider)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _options = options;
            _provider = provider;
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
                ResultView.Mensagem =
                    MensagemViewHelper.SetBadRequest("Ao informar a Senha do Usuário, é preciso informar o Login");

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
                            (!string.IsNullOrWhiteSpace(Login) ? x.Login == Login : true))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoUsuario);

                return ResultView;
            }

            var dataAtual = DateTime.UtcNow.AddHours(-3);

            await _context.Usuario
                .Where(x => x.UsuarioId == result.UsuarioId)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.DataUltimoAcesso, dataAtual));

            result.DataUltimoAcesso = dataAtual;

            List<SistemaPerfilAcessoUsuariosModel> resultPerfilDeAcesso = await _context.PerfilAcessoUsuario
                .Where(x => x.UsuarioId == result.UsuarioId)
                .Include(x => x.PerfilAcesso)
                .AsNoTracking()
                .ToListAsync();

            List<TiposContatoPessoaModel> resultTiposContatos = new();

            if (result.PessoaId != null)
            {
                resultTiposContatos = await _context.TipoPessoaContatos
                    .Where(x => x.PessoaId == result.PessoaId.Value && x.TiposContatos != null &&
                                x.TiposContatos.FlagAtivo == 'S')
                    .Include(x => x.TiposContatos)
                    .OrderBy(x => x.Descricao)
                    .AsNoTracking()
                    .ToListAsync();
            }

            ResultView = _mapper.Map<UsuarioDTO>(result);

            ResultView.InformacoesUsuario ??= new InformacoesUsuarioDTO();

            ResultView.InformacoesUsuario.Perfis =
                _mapper.Map<List<PerfisAcessoUsuarioDTO>>(resultPerfilDeAcesso);

            ResultView.InformacoesUsuario.Contatos =
                _mapper.Map<List<TiposContatosPessoaDTO>>(resultTiposContatos);

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            int? isSenhaInicial = _context.Database.SqlQueryRaw<int>(
                "SELECT TOP 1 1 AS Value FROM dbo.tb_dep_usuarios WHERE id_usuario = @id AND senha1 = HASHBYTES('MD5', 'INICIAL123')",
                new SqlParameter("@id", result.UsuarioId)
            ).FirstOrDefault();

            if (isSenhaInicial == 1)
            {
                ResultView.Mensagem.AvisosInformativos.Add(
                    "Alterar Senha.");
            }


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

        public async Task<PerfilAcessoListDTO> ListAccessProfileAsync(int usuarioId, byte? skip, byte? take,
            CancellationToken ct)
        {
            PerfilAcessoListDTO ResultView = new();

            var possuiPermissao = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == usuarioId
                               && x.PerfilAcessoId == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                                             && s.IdSubModulo == (int)SubModuloEnum.VerPerfisDeAcessoHomolog),
                    cancellationToken: ct);

            if (!possuiPermissao)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Não possui permissão");
                return ResultView;
            }

            var limit = take.HasValue && take.Value > 0 ? take.Value : 20;
            var offset = skip.HasValue && skip.Value >= 0 ? skip.Value : 0;
            var result = await _context.PerfilAcesso
                .Where(x => x.FlagAtivo == 'S')
                .Skip(offset)
                .Take(limit)
                .AsNoTracking()
                .ProjectTo<PerfilAcessoDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            ResultView.Listagem = result;

            ResultView.Mensagem = result.Count > 0
                ? MensagemViewHelper.SetFound(result.Count)
                : MensagemViewHelper.SetNotFound();

            return ResultView;
        }


        public async Task<UsuarioGerenciamentoDTO> GetByLoginForManagementAsync(
            int usuarioId,
            string login,
            CancellationToken ct)
        {
            UsuarioGerenciamentoDTO ResultView = new();
            login = login.ToUpperTrim();

            var possuiPermissao = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == usuarioId
                               && x.PerfilAcessoId == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                                             && s.IdSubModulo == (int)SubModuloEnum.VerPerfisDeAcessoHomolog),
                    cancellationToken: ct);

            if (!possuiPermissao)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Não possui permissão");
                return ResultView;
            }

            var result = await _context.Usuario
                .AsNoTracking()
                .Where(x => x.Login == login)
                .Select(x => new UsuarioGerenciamentoDTO
                {
                    Login = x.Login,
                    Nome = x.Pessoa.Nome,
                    Matricula = x.Matricula,
                    DataUltimoAcesso = DateTimeHelper.FormatDateTime(x.DataUltimoAcesso,
                        DateTimeHelper.DateTimeFormat.DateFormatted),
                    FlagAtivo = x.FlagAtivo,
                    ClientesVinculados = x.ListagemUsuarioCliente
                        .Select(uc => new ClienteVincularUsuarioDTO
                        {
                            IdentificadorCliente = uc.Cliente.ClienteId,
                            Nome = uc.Cliente.Nome,
                            FlagAtivo = uc.Cliente.FlagAtivo
                        }).ToList(),
                    DepositosVinculados = x.ListagemUsuarioDeposito
                        .Select(ud => new DepositoVincularAUsuariosDTO
                        {
                            IdentificadorDeposito = ud.Deposito.DepositoId,
                            Nome = ud.Deposito.Nome,
                            FlagAtivo = ud.Deposito.FlagAtivo
                        }).ToList(),
                    PerfisDeAcessoVinculados = _context.PerfilAcessoUsuario
                        .Where(p => p.UsuarioId == x.UsuarioId)
                        .Select(p => new PerfilAcessoDTO
                        {
                            PerfilAcessoId = p.PerfilAcessoId,
                            Descricao = p.PerfilAcesso.Descricao
                        }).ToList()
                })
                .FirstOrDefaultAsync(ct);
            if (result is null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Usuário não encontrado");
                return ResultView;
            }

            ResultView = result;
            ResultView.Mensagem = MensagemViewHelper.SetFound();
            return ResultView;
        }

        public async Task<UsuarioPorNomeOuLoginListDTO> ListByUsernameOrLogin(
            int usuarioId,
            ConsultaPorNomeOuLoginParameters request,
            CancellationToken ct)
        {
            UsuarioPorNomeOuLoginListDTO ResultView = new();

            var possuiPermissao = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == usuarioId
                               && x.PerfilAcessoId == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                                             && s.IdSubModulo == (int)SubModuloEnum.VerPerfisDeAcessoHomolog),
                    cancellationToken: ct);

            if (!possuiPermissao)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Não possui permissão");
                return ResultView;
            }

            if (string.IsNullOrWhiteSpace(request.Login) && string.IsNullOrWhiteSpace(request.Username))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Informe o Login ou o Username");
                return ResultView;
            }

            var login = request.Login?.Trim();
            var username = request.Username?.Trim();

            var query = _context.Usuario
                .AsNoTracking();

            if (!string.IsNullOrEmpty(login))
            {
                query = query.Where(x => x.Login.Contains(login));
            }

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(x => x.Pessoa.Nome.Contains(username));
            }

            if (!request.UsuariosInativos)
            {
                query = query.Where(x => x.FlagAtivo == "S");
            }

            var limit = request.Take.HasValue && request.Take.Value > 0 ? request.Take.Value : 20;
            var offset = request.Skip.HasValue && request.Skip.Value >= 0 ? request.Skip.Value : 0;

            var usuarios = await query
                .OrderByDescending(x => x.DataUltimoAcesso)
                .Skip(offset)
                .Take(limit)
                .Select(x => new UsuarioPorNomeOuLoginDTO
                {
                    Login = x.Login,
                    Nome = x.Pessoa.Nome,
                    FlagAtivo = x.FlagAtivo,
                    DataUltimoAcesso = DateTimeHelper.FormatDateTime(x.DataUltimoAcesso,
                        DateTimeHelper.DateTimeFormat.DateTimeFormatted)
                })
                .ToListAsync(cancellationToken: ct);

            if (usuarios is null || usuarios.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Nenhum usuário encontrado");
                return ResultView;
            }

            ResultView.Listagem = usuarios;
            ResultView.Mensagem = MensagemViewHelper.SetFound(usuarios.Count);
            return ResultView;
        }

        public async Task<TiposDePermissãoListDTO> ListPermissionsTypes(CancellationToken ct)
        {
            TiposDePermissãoListDTO ResultView = new();

            var result = await _context.UsuarioTipoPermissao
                .AsNoTracking()
                .Select(x => new TipoPermissaoDTO
                {
                    IdentificadorTipoPermissao = x.TipoPermissaoId,
                    Codigo = x.Codigo,
                    Descricao = x.Descricao
                })
                .ToListAsync(ct);

            ResultView.Listagem = result;

            ResultView.Mensagem = result.Count > 0
                ? MensagemViewHelper.SetFound(result.Count)
                : MensagemViewHelper.SetNotFound();

            return ResultView;
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

        public async Task<MensagemDTO> ActivateMfa(int usuarioId)
        {
            MensagemDTO ResultView = new();
            var user = await _context.Usuario.FirstOrDefaultAsync(x => x.UsuarioId == usuarioId);

            if (user is null)
            {
                ResultView = MensagemViewHelper.SetNotFound();
                return ResultView;
            }

            user.FlagMfa = 'S';
            user.DataAlteracao = DateTime.UtcNow.Add(TimeSpan.FromHours(-3));
            try
            {
                await _context.SaveChangesAsync();
                return MensagemViewHelper.SetUpdateSuccess();
            }
            catch (Exception e)
            {
                ResultView = MensagemViewHelper.SetBadRequest();
                return ResultView;
            }
        }

        public async Task<MensagemDTO> DeactivateMfa(int usuarioId)
        {
            MensagemDTO ResultView = new();
            var user = await _context.Usuario.FirstOrDefaultAsync(x => x.UsuarioId == usuarioId);

            if (user is null)
            {
                ResultView = MensagemViewHelper.SetNotFound();
                return ResultView;
            }

            user.FlagMfa = 'N';
            user.DataAlteracao = DateTime.UtcNow.Add(TimeSpan.FromHours(-3));
            try
            {
                await _context.SaveChangesAsync();
                return MensagemViewHelper.SetUpdateSuccess();
            }
            catch (Exception e)
            {
                ResultView = MensagemViewHelper.SetBadRequest();
                return ResultView;
            }
        }

        public async Task<MensagemDTO> GenerateMfaCode(int usuarioId)
        {
            MensagemDTO ResultView = new();

            var exists = _context.Usuario.Any(x => x.UsuarioId == usuarioId);
            if (!exists)
                return MensagemViewHelper.SetNotFound("Usuario não encontrado");

            var telefone = await _provider
                .GetService<PessoaService>()
                .GetPessoaTelefoneByIdAsync(usuarioId);

            if (string.IsNullOrWhiteSpace(telefone))
                return MensagemViewHelper.SetNotFound();
            var codigo = StringHelper.GenerateNumericCode(6);
            var expiresAt = DateTime.UtcNow.AddHours(-3).AddMinutes(3);
            string message = $"🔒 *Webzi Segurança*\n\n" +
                             $"Seu código de acesso é: *{codigo}*\n\n" +
                             $"Este código expira em 3 minutos.\n" +
                             $"Não compartilhe este código.";
            var codeHash = GenerateSha256Hex($"enable:{usuarioId}:{codigo}:{_options.Value.Secret}");

            var result = await RegistrarMfaCodeAsync(usuarioId, codeHash, expiresAt);
            if (result.AvisosImpeditivos.Count > 0)
            {
                return result;
            }

            try
            {
                await _provider.GetService<WhatsAppService>().SendTextMessageAsync(telefone,
                    message);
            }
            catch (Exception e)
            {
                ResultView = MensagemViewHelper.SetBadRequest($"[Envio da Mensagem]: {e.Message}");
                return ResultView;
            }

            return MensagemViewHelper.SetCreateSuccess();
        }

        public async Task<MensagemDTO> ValidMfaCode(ConfirmarCodigoMfaParameters request)
        {
            var codeHash = GenerateSha256Hex($"enable:{request.UsuarioId}:{request.Codigo}:{_options.Value.Secret}");
            var result = await SearchMfaCode(request.UsuarioId, codeHash);
            return result;
        }

        private async Task<MensagemDTO> SearchMfaCode(int usuarioId, string codeHash)
        {
            var exists = await _context.AuthMfaCodes
                .AsTracking()
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(x => x.UsuarioId == usuarioId);
            if (exists == null)
                return MensagemViewHelper.SetNotFound();
            if (exists.Validated)
                return MensagemViewHelper.SetOk();
            if (exists.Attempts >= 5)
                return MensagemViewHelper.SetBadRequest("Limite de tentativas excedido.");
            var now = DateTime.UtcNow.AddHours(-3);
            if (exists.ExpiresAt <= now)
            {
                exists.Attempts++;
                await _context.SaveChangesAsync();
                return MensagemViewHelper.SetBadRequest("Tempo limite excedido.");
            }

            if (exists.CodeHash != codeHash)
            {
                exists.Attempts++;
                await _context.SaveChangesAsync();
                return MensagemViewHelper.SetBadRequest("Código inválido.");
            }

            exists.Attempts++;
            exists.Validated = true;
            var result = await _context.SaveChangesAsync();
            return result <= 0 ? MensagemViewHelper.SetBadRequest("Problema atualizar") : MensagemViewHelper.SetOk();
        }

        private async Task<MensagemDTO> RegistrarMfaCodeAsync(int usuarioId, string codeHash, DateTime expiresAt)
        {
            if (usuarioId <= 0 || codeHash.Length <= 0 || expiresAt < DateTime.UtcNow.AddHours(-3))
                return MensagemViewHelper.SetBadRequest();

            AuthMfaCodesModel auth = new()
            {
                CodeHash = codeHash,
                Attempts = 1,
                ExpiresAt = expiresAt,
                CreatedAt = DateTime.UtcNow.AddHours(-3),
                UsuarioId = usuarioId
            };

            await _context.AuthMfaCodes.AddAsync(auth);
            var result = await _context.SaveChangesAsync();
            if (result <= 0)
                MensagemViewHelper.SetBadRequest("Erro ao registrar mfa code");
            return MensagemViewHelper.SetCreateSuccess();
        }

        private string GenerateSha256Hex(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            var sb = new StringBuilder(hash.Length * 2);
            foreach (var x in hash)
                sb.Append(x.ToString("x2"));

            return sb.ToString();
        }

        private string GenerateSqlServerMd5Hash(string input)
        {
            using var md5 = MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(bytes);
            return Encoding.Default.GetString(hashBytes);
        }

        private async Task FiltrarDepositosSemVinculo(List<int> vincularCliente, List<int> vincularDeposito,
            CancellationToken ct)
        {
            if (vincularDeposito?.Count > 0)
            {
                if (vincularCliente == null || vincularCliente.Count == 0)
                {
                    vincularDeposito.Clear();
                    return;
                }

                var depositosValidos = await _context.ClienteDeposito
                    .AsNoTracking()
                    .Where(x => vincularDeposito.Contains(x.DepositoId) && vincularCliente.Contains(x.ClienteId))
                    .Select(x => x.DepositoId)
                    .Distinct()
                    .ToListAsync(ct);

                vincularDeposito.RemoveAll(depositoId => !depositosValidos.Contains(depositoId));
            }
        }

        public async Task<MensagemDTO> CreateUserAsync(
            int usuarioCadastroId,
            CadastroUsuarioParameters parameters,
            CancellationToken ct)
        {
            MensagemDTO ResultView = new();

            var possuiPermissao = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == usuarioCadastroId
                               && x.PerfilAcessoId == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                                             && s.IdSubModulo == (int)SubModuloEnum.CadastrarUsuarioHomolog),
                    cancellationToken: ct);

            if (!possuiPermissao)
            {
                ResultView = MensagemViewHelper.SetBadRequest("Não possui permissão");
                return ResultView;
            }

            if (string.IsNullOrWhiteSpace(parameters.Login))
            {
                ResultView = MensagemViewHelper.SetBadRequest("Informe o Login do usuário");
                return ResultView;
            }

            if (parameters.identificadorPessoa <= 0)
            {
                ResultView = MensagemViewHelper.SetBadRequest("Informe a pessoa que seria vinculada ao usuário");
                return ResultView;
            }

            var loginNormalized = parameters.Login.ToUpperTrim();

            var loginExists = await _context.Usuario
                .AsNoTracking()
                .AnyAsync(x => x.Login == loginNormalized, ct);

            if (loginExists)
            {
                ResultView = MensagemViewHelper.SetBadRequest("O login informado já está em uso");
                return ResultView;
            }

            parameters.VincularCliente ??= new List<int>();
            parameters.VincularDeposito ??= new List<int>();
            parameters.VincularCliente = parameters.VincularCliente.Distinct().ToList();
            parameters.VincularDeposito = parameters.VincularDeposito.Distinct().ToList();
            parameters.PerfisDeAcesso = parameters.PerfisDeAcesso?.Distinct().ToList() ?? new List<int>();

            await FiltrarDepositosSemVinculo(parameters.VincularCliente, parameters.VincularDeposito, ct);

            var permissaoConfig = parameters.PermissoesUsuario?.FirstOrDefault();

            var novoUsuario = new UsuarioModel
            {
                Login = loginNormalized,
                Senha1 = "INICIAL123",
                PessoaId = parameters.identificadorPessoa,
                Matricula = parameters.Matricula,
                FlagAtivo = "S",
                FlagPermissaoDesconto = permissaoConfig?.FlagPermissaoDesconto ?? "N",
                FlagPermissaoDataRetroativaFaturamento = permissaoConfig?.FlagPermissaoDataRetroativaFaturamento ?? "N",
                UsuarioCadastroId = usuarioCadastroId,
                DataCadastro = DateTime.UtcNow.AddHours(-3)
            };

            await using var _transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                await _context.Usuario.AddAsync(novoUsuario, ct);
                await _context.SaveChangesAsync(ct);

                var newUserId = novoUsuario.UsuarioId;

                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"UPDATE dbo.tb_dep_usuarios SET senha1 = HASHBYTES('MD5', 'INICIAL123') WHERE id_usuario = {newUserId}",
                    ct);

                if (parameters.PerfisDeAcesso.Count > 0)
                {
                    var perfisList = parameters.PerfisDeAcesso.Select(perfilId => new SistemaPerfilAcessoUsuariosModel
                    {
                        UsuarioId = newUserId,
                        PerfilAcessoId = perfilId
                    }).ToList();

                    await _context.PerfilAcessoUsuario.AddRangeAsync(perfisList, ct);
                }

                if (parameters.VincularCliente.Count > 0)
                {
                    var clientesList = parameters.VincularCliente.Select(clienteId => new UsuarioClienteModel
                    {
                        UsuarioId = newUserId,
                        ClienteId = clienteId,
                        UsuarioCadastroId = usuarioCadastroId,
                        DataCadastro = DateTime.UtcNow.AddHours(-3)
                    }).ToList();

                    await _context.UsuarioCliente.AddRangeAsync(clientesList, ct);
                }

                if (parameters.VincularDeposito.Count > 0)
                {
                    var depositosList = parameters.VincularDeposito.Select(depositoId => new UsuarioDepositoModel
                    {
                        UsuarioId = newUserId,
                        DepositoId = depositoId,
                        UsuarioCadastroId = usuarioCadastroId,
                        DataCadastro = DateTime.UtcNow.AddHours(-3)
                    }).ToList();

                    await _context.UsuarioDeposito.AddRangeAsync(depositosList, ct);
                }

                await _context.SaveChangesAsync(ct);

                await _transaction.CommitAsync(ct);
                ResultView = MensagemViewHelper.SetCreateSuccess();
                return ResultView;
            }
            catch (Exception e)
            {
                ResultView = MensagemViewHelper.SetBadRequest(e.Message);
                return ResultView;
            }
        }

        public async Task<MensagemDTO> UpdateUserAsync(
            int usuarioAlteracaoId,
            AtualizarUsuarioParameters parameters,
            CancellationToken ct)
        {
            MensagemDTO ResultView = new();

            var possuiPermissao = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == usuarioAlteracaoId
                               && x.PerfilAcessoId == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                                             && s.IdSubModulo == (int)SubModuloEnum.EditarUsuarioHomolog),
                    cancellationToken: ct);

            if (!possuiPermissao)
            {
                ResultView = MensagemViewHelper.SetBadRequest("Não possui permissão");
                return ResultView;
            }

            if (parameters.identificadorUsuario <= 0)
            {
                ResultView = MensagemViewHelper.SetBadRequest("Identificador do usuário inválido");
                return ResultView;
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(x => x.UsuarioId == parameters.identificadorUsuario, ct);

            if (usuario == null)
            {
                ResultView = MensagemViewHelper.SetNotFound("Usuário não encontrado");
                return ResultView;
            }

            if (!string.IsNullOrWhiteSpace(parameters.Login))
            {
                var loginNormalized = parameters.Login.ToUpperTrim();
                var loginExists = await _context.Usuario
                    .AsNoTracking()
                    .AnyAsync(x => x.Login == loginNormalized && x.UsuarioId != usuario.UsuarioId, ct);

                if (loginExists)
                {
                    ResultView =
                        MensagemViewHelper.SetBadRequest("O login informado já está em uso por outro usuário");
                    return ResultView;
                }

                usuario.Login = loginNormalized;
            }

            if (parameters.identificadorPessoa > 0)
            {
                usuario.PessoaId = parameters.identificadorPessoa;
            }

            if (parameters.Matricula != null)
            {
                usuario.Matricula = parameters.Matricula;
            }

            if (!string.IsNullOrWhiteSpace(parameters.FlagAtivo))
            {
                usuario.FlagAtivo = parameters.FlagAtivo.ToUpperTrim();
            }

            if (parameters.PermissoesUsuario?.Count > 0)
            {
                var perm = parameters.PermissoesUsuario.First();
                if (!string.IsNullOrWhiteSpace(perm.FlagPermissaoDesconto))
                {
                    usuario.FlagPermissaoDesconto = perm.FlagPermissaoDesconto;
                }

                if (!string.IsNullOrWhiteSpace(perm.FlagPermissaoDataRetroativaFaturamento))
                {
                    usuario.FlagPermissaoDataRetroativaFaturamento = perm.FlagPermissaoDataRetroativaFaturamento;
                }
            }

            usuario.UsuarioAlteracaoId = usuarioAlteracaoId;
            usuario.DataAlteracao = DateTime.UtcNow.AddHours(-3);

            parameters.VincularCliente ??= new List<int>();
            parameters.VincularDeposito ??= new List<int>();
            parameters.VincularCliente = parameters.VincularCliente.Distinct().ToList();
            parameters.VincularDeposito = parameters.VincularDeposito.Distinct().ToList();
            parameters.PerfisDeAcesso = parameters.PerfisDeAcesso?.Distinct().ToList() ?? new List<int>();

            await FiltrarDepositosSemVinculo(parameters.VincularCliente, parameters.VincularDeposito, ct);

            await using var _transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                var perfisAtuais = await _context.PerfilAcessoUsuario
                    .Where(x => x.UsuarioId == usuario.UsuarioId)
                    .ToListAsync(ct);
                _context.PerfilAcessoUsuario.RemoveRange(perfisAtuais);

                if (parameters.PerfisDeAcesso.Count > 0)
                {
                    var novosPerfis = parameters.PerfisDeAcesso.Select(perfilId => new SistemaPerfilAcessoUsuariosModel
                    {
                        UsuarioId = usuario.UsuarioId,
                        PerfilAcessoId = perfilId
                    }).ToList();
                    await _context.PerfilAcessoUsuario.AddRangeAsync(novosPerfis, ct);
                }

                var clientesAtuais = await _context.UsuarioCliente
                    .Where(x => x.UsuarioId == usuario.UsuarioId)
                    .ToListAsync(ct);
                _context.UsuarioCliente.RemoveRange(clientesAtuais);

                if (parameters.VincularCliente.Count > 0)
                {
                    var novosClientes = parameters.VincularCliente.Select(clienteId => new UsuarioClienteModel
                    {
                        UsuarioId = usuario.UsuarioId,
                        ClienteId = clienteId,
                        UsuarioCadastroId = usuarioAlteracaoId,
                        DataCadastro = DateTime.UtcNow.AddHours(-3)
                    }).ToList();
                    await _context.UsuarioCliente.AddRangeAsync(novosClientes, ct);
                }

                var depositosAtuais = await _context.UsuarioDeposito
                    .Where(x => x.UsuarioId == usuario.UsuarioId)
                    .ToListAsync(ct);
                _context.UsuarioDeposito.RemoveRange(depositosAtuais);

                if (parameters.VincularDeposito.Count > 0)
                {
                    var novosDepositos = parameters.VincularDeposito.Select(depositoId => new UsuarioDepositoModel
                    {
                        UsuarioId = usuario.UsuarioId,
                        DepositoId = depositoId,
                        UsuarioCadastroId = usuarioAlteracaoId,
                        DataCadastro = DateTime.UtcNow.AddHours(-3)
                    }).ToList();
                    await _context.UsuarioDeposito.AddRangeAsync(novosDepositos, ct);
                }

                await _context.SaveChangesAsync(ct);
                await _transaction.CommitAsync(ct);

                ResultView = MensagemViewHelper.SetUpdateSuccess();
                return ResultView;
            }
            catch (Exception e)
            {
                ResultView = MensagemViewHelper.SetBadRequest(e.Message);
                return ResultView;
            }
        }

        public async Task<MensagemDTO> ResetPasswordAsync(int usuarioId, RedefinirSenhaParameters parameters,
            CancellationToken ct)
        {
            MensagemDTO ResultView = new();
            parameters.Login = parameters.Login.ToUpperTrim();

            var possuiPermissao = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == usuarioId
                               && x.PerfilAcessoId == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                                             && s.IdSubModulo == (int)SubModuloEnum.ResetarSenhaDoUsuarioHomolog),
                    cancellationToken: ct);

            if (!possuiPermissao)
            {
                ResultView = MensagemViewHelper.SetBadRequest("Não possui permissão para redefinir senha");
                return ResultView;
            }

            var dataAlteracao = DateTime.UtcNow.AddHours(-3);

            var rowsAffected = await _context.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE dbo.tb_dep_usuarios SET senha1 = HASHBYTES('MD5', 'INICIAL123'), id_usuario_alteracao = {usuarioId}, data_alteracao = {dataAlteracao} WHERE login = {parameters.Login}",
                ct);

            if (rowsAffected == 0)
            {
                ResultView = MensagemViewHelper.SetNotFound("Usuário não encontrado");
                return ResultView;
            }

            ResultView = MensagemViewHelper.SetUpdateSuccess();
            return ResultView;
        }

        public async Task<MensagemDTO> ChangePasswordAsync(CadastrarSenhaParameters parameters,
            CancellationToken ct)
        {
            MensagemDTO ResultView = new();

            var loginNormalized = parameters.Login.ToUpperTrim();
            var novaSenha = parameters.Senha.ToUpperTrim();
            var dataAlteracao = DateTime.UtcNow.AddHours(-3);

            SqlParameter[] sqlParameters = new[]
            {
                new SqlParameter("@novaSenha", SqlDbType.VarChar) { Value = novaSenha },
                new SqlParameter("@dataAlteracao", SqlDbType.DateTime) { Value = dataAlteracao },
                new SqlParameter("@login", SqlDbType.VarChar) { Value = loginNormalized }
            };

            var result = await _context.Database.ExecuteSqlRawAsync(
                """
                UPDATE dbo.tb_dep_usuarios
                SET senha1 = HASHBYTES('MD5', @novaSenha),
                    data_alteracao = @dataAlteracao
                WHERE login = @login
                """,
                sqlParameters,
                ct);

            if (result == 0)
            {
                ResultView = MensagemViewHelper.SetNotFound("Usuário não encontrado");
                return ResultView;
            }

            ResultView = MensagemViewHelper.SetUpdateSuccess();
            return ResultView;
        }
    }
}
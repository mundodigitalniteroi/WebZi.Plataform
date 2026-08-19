using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.ViewModel.Usuario;
using WebZi.Plataform.Domain.ViewModel.Usuario.AtualizarUsuario;
using WebZi.Plataform.Domain.ViewModel.Usuario.CadastrarSenha;
using WebZi.Plataform.Domain.ViewModel.Usuario.CadastroUsuario;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _configuration;

        public UsuarioController(IServiceProvider provider, IConfiguration configuration)
        {
            _provider = provider;
            _configuration = configuration;
        }

        [HttpGet("SelecionarPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<UsuarioDTO>> SelecionarPorIdentificador(int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetByIdAsync(IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorLogin")]
        // TODO: [Authorize]
        public async Task<ActionResult<UsuarioDTO>> SelecionarPorLogin(string Login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetByUsernameAsync(Login);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("ListarPorLoginOuUsername")]
        public async Task<ActionResult<UsuarioPorNomeOuLoginListDTO>> ListarPorLoginOuUsername(
            ConsultaPorNomeOuLoginParameters request, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioPorNomeOuLoginListDTO ResultView = new();

            var usuarioId = User.GetUserId();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .ListByUsernameOrLogin(usuarioId!.Value, request, ct);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ConsultarUsuarioParaGerenciamento")]
        public async Task<ActionResult<UsuarioGerenciamentoDTO>> ConsultarUsuarioParaGerenciamento(string login,
            CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioGerenciamentoDTO ResultView = new();
            var usuarioId = User.GetUserId();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetByLoginForManagementAsync(usuarioId!.Value, login, ct);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("TiposDePermissoes")]
        public async Task<ActionResult<TiposDePermissãoListDTO>> TiposDePermissoes(CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TiposDePermissãoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .ListPermissionsTypes(ct);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListaPerfisDeAcesso")]
        public async Task<ActionResult<PerfilAcessoListDTO>> ListaPerfisDeAcesso(byte? skip, byte? take,
            CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PerfilAcessoListDTO ResultView = new();
            var userId = User.GetUserId();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .ListAccessProfileAsync(userId!.Value, skip, take, ct);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<UsuarioDTO>> Login([FromBody] UsuarioLoginParameters Login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetByCredentialsAsync(Login.Usuario, Login.Senha);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }


        [HttpPost("Cadastrar")]
        public async Task<ActionResult<MensagemDTO>> Cadastrar(CadastroUsuarioParameters parameters,
            CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            var usuarioId = User.GetUserId();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .CreateUserAsync(usuarioId!.Value, parameters, ct);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult<MensagemDTO>> Atualizar(AtualizarUsuarioParameters parameters,
            CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            var usuarioId = User.GetUserId();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .UpdateUserAsync(usuarioId!.Value, parameters, ct);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPut("RedefinirSenha")]
        public async Task<ActionResult<MensagemDTO>> RedefinirSenha(
            RedefinirSenhaParameters parameters,
            CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            var usuarioId = User.GetUserId();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .ResetPasswordAsync(usuarioId!.Value, parameters, ct);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPut("AlterarSenha")]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> AlterarSenha(
            CadastrarSenhaParameters parameters,
            CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .ChangePasswordAsync(parameters, ct);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("GerarCodigoMfa")]
        public async Task<ActionResult<MensagemDTO>> GerarCodigoMfa(GerarCodigoMfaParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            // var userId = User.GetUserId();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GenerateMfaCode(parameters.UsuarioId);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("ConfirmarCodigoMfa")]
        public async Task<ActionResult<MensagemDTO>> ConfirmarCodigoMfa(ConfirmarCodigoMfaParameters request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .ValidMfaCode(request);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPatch("AtivarMfa")]
        public async Task<ActionResult<MensagemDTO>> AtivarMfa([FromQuery] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            // var userId = User.GetUserId();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .ActivateMfa(userId);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPatch("DesativarMfa")]
        public async Task<ActionResult<MensagemDTO>> DesativarMfa([FromQuery] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            // var userId = User.GetUserId();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .DeactivateMfa(userId);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("DesvincularPerfilAcesso")]
        public async Task<ActionResult<MensagemDTO>> DesvincularPerfilAcesso([FromBody] DesvincularPerfisDoUsuarioParameters parameters, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .UnlinkProfilesToUserAsync(parameters, ct);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);
                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("DesvincularClientes")]
        public async Task<ActionResult<MensagemDTO>> DesvincularClientes([FromBody] DesvincularClienteDoUsuarioParameters parameters, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .UnlinkClientToUserAsync(parameters, ct);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);
                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("DesvincularDepositos")]
        public async Task<ActionResult<MensagemDTO>> DesvincularDepositos([FromBody] DesvincularDepositoDoUsuarioParameters parameters, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .UnlinkDepositToUserAsync(parameters, ct);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);
                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("DesvincularPermissao")]
        public async Task<ActionResult<MensagemDTO>> DesvincularPermissao([FromBody] DesvincularPermissaoDoUsuarioParameters parameters, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .UnlinkPermissionToUserAsync(parameters, ct);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);
                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }
    }
}
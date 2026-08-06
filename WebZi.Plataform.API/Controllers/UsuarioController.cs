using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.ViewModel.Usuario;

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

        [HttpPost("SelecionarPorLoginOuUsername")]
        // TODO: [Authorize]
        public async Task<ActionResult<UsuarioPorNomeOuLoginListDTO>> SelecionarPorLoginOuUsername(ConsultaPorNomeOuLoginParameters request, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioPorNomeOuLoginListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetByUsernameOrLogin(request, ct);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
        
        [HttpGet("ListaPerfisDeAcesso")]
        // TODO: [Authorize]
        public async Task<ActionResult<UsuarioPorNomeOuLoginListDTO>> ListaPerfisDeAcesso(CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioPorNomeOuLoginListDTO ResultView = new();
            var userId = User.GetUserId();
            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .ListAccessProfileAsync(userId!.Value, ct);

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
    }
}
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel.Usuario;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public UsuarioController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("SelecionarPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<UsuarioViewModel>> SelecionarPorIdentificador(int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetByIdAsync(IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorLogin")]
        // TODO: [Authorize]
        public async Task<ActionResult<UsuarioViewModel>> SelecionarPorLogin(string Login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetByLoginAsync(Login);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorLoginSenha")]
        // TODO: [Authorize]
        public async Task<ActionResult<UsuarioViewModel>> SelecionarPorLoginSenha(string Login, string Senha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetByLoginAsync(Login, Senha);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
    }
}
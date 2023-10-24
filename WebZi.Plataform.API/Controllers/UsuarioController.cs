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
        public async Task<ActionResult<UsuarioViewModel>> SelecionarPorIdentificador(int IdentificadorUsuario)
        {
            UsuarioViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetById(IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorLogin")]
        public async Task<ActionResult<UsuarioViewModel>> SelecionarPorLogin(string Login)
        {
            UsuarioViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetByLogin(Login);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorLoginSenha")]
        public async Task<ActionResult<UsuarioViewModel>> SelecionarPorLoginSenha(string Login, string Senha)
        {
            UsuarioViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<UsuarioService>()
                    .GetByLogin(Login, Senha);

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

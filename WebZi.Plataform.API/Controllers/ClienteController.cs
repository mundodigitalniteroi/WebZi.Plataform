using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Domain.DTO.Cliente;
using WebZi.Plataform.Domain.DTO.Generic;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public ClienteController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("Listar")]
        // TODO: [Authorize]
        public async Task<ActionResult<ClienteListDTO>> Listar(int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClienteListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ClienteService>()
                    .ListAsync(IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarLogomarca")]
        // TODO: [Authorize]
        public async Task<ActionResult<ImageListDTO>> SelecionarLogomarca(int IdentificadorCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ClienteService>()
                    .GetLogomarcaAsync(IdentificadorCliente);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<ClienteListDTO>> SelecionarPorIdentificador(int IdentificadorCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClienteListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ClienteService>()
                    .GetByIdAsync(IdentificadorCliente);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorNome")]
        // TODO: [Authorize]
        public async Task<ActionResult<ClienteListDTO>> SelecionarPorNome(string Nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClienteListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ClienteService>()
                    .GetByNameAsync(Nome);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
    }
}
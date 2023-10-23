using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Domain.ViewModel.Banco;
using WebZi.Plataform.Domain.ViewModel.Cliente;

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

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<ClienteViewModelList>> SelecionarPorId(int ClienteId)
        {
            ClienteViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ClienteService>()
                    .GetById(ClienteId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorNome")]
        public async Task<ActionResult<ClienteViewModelList>> SelecionarPorNome(string Nome)
        {
            ClienteViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ClienteService>()
                    .GetByName(Nome);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ClienteViewModelList>> Listar(int UsuarioId)
        {
            ClienteViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ClienteService>()
                    .List(UsuarioId);

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
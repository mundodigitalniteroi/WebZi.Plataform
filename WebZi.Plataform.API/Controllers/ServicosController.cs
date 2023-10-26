using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Servico;
using WebZi.Plataform.Domain.ViewModel.Servico;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicosController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public ServicosController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("ListarReboque")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueViewModelList>> ListarReboque(int IdentificadorCliente, int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .ListarReboqueAsync(IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarReboquista")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboquistaViewModelList>> ListarReboquista(int IdentificadorCliente, int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboquistaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .ListarReboquistaAsync(IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueViewModelList>> SelecionarReboquePorIdentificador(int IdentificadorReboque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboqueByIdAsync(IdentificadorReboque);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorPlaca")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueViewModelList>> SelecionarReboquePorPlaca(string Placa, int IdentificadorCliente, int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboqueByPlacaAsync(Placa, IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquistaPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboquistaViewModelList>> SelecionarReboquistaPorIdentificador(int IdentificadorReboquista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboquistaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetByReboquistaIdAsync(IdentificadorReboquista);

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
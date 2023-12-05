using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Domain.ViewModel.Deposito;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public DepositoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("Listar")]
        // TODO: [Authorize]
        public async Task<ActionResult<DepositoViewModelList>> Listar(int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DepositoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DepositoService>()
                    .ListAsync(IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarDataHoraPeloIdentificador")]
        // TODO: [Authorize]
        public ActionResult<DateTime> SelecionarDataHoraPeloIdentificador(int Identificador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Identificador <= 0)
            {
                return BadRequest("Identificador do Depósito inválido");
            }

            try
            {
                DateTime result = _provider
                    .GetService<DepositoService>()
                    .GetDataHoraPorDeposito(Identificador);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)Mensagem.HtmlStatusCode, Mensagem);
            }
        }

        [HttpGet("SelecionarPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<DepositoViewModelList>> SelecionarPorIdentificador(int Identificador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DepositoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DepositoService>()
                    .GetByIdAsync(Identificador);

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
        public async Task<ActionResult<DepositoViewModelList>> SelecionarPorNome(string Nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DepositoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DepositoService>()
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
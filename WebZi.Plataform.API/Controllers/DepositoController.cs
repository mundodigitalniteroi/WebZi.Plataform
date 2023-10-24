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
        public async Task<ActionResult<DepositoViewModelList>> Listar(int IdentificadorUsuario)
        {
            DepositoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DepositoService>()
                    .List(IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarDataHoraPeloIdentificador")]
        public ActionResult<DateTime> SelecionarDataHoraPeloIdentificador(int Identificador)
        {
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
                var Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)Mensagem.HtmlStatusCode, Mensagem);
            }
        }

        [HttpGet("SelecionarPorIdentificador")]
        public async Task<ActionResult<DepositoViewModelList>> SelecionarPorIdentificador(int Identificador)
        {
            DepositoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DepositoService>()
                    .GetById(Identificador);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorNome")]
        public async Task<ActionResult<DepositoViewModelList>> SelecionarPorNome(string Nome)
        {
            DepositoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DepositoService>()
                    .GetByName(Nome);

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
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

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<DepositoViewModelList>> SelecionarPorId(int DepositoId)
        {
            DepositoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DepositoService>()
                    .GetById(DepositoId);

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

        [HttpGet("Listar")]
        public async Task<ActionResult<DepositoViewModelList>> Listar()
        {
            DepositoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DepositoService>()
                    .List();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GetDateTimeById")]
        public async Task<ActionResult<DateTime>> GetDateTimeById(int DepositoId)
        {
            if (DepositoId <= 0)
            {
                return BadRequest("Identificador do Depósito inválido");
            }

            DateTime result = await _provider
                .GetService<DepositoService>()
                .GetDataHoraPorDeposito(DepositoId);

            return Ok(result);
        }
    }
}
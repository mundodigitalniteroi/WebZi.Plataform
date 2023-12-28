using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.WebServices.DetranRio;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetranRioController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public DetranRioController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("ConsultarVeiculoPorChassi")]
        // TODO: [Authorize]
        public async Task<ActionResult<DetranRioVeiculoViewModel>> ConsultarVeiculoPorChassi(string Chassi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DetranRioVeiculoViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DetranRioService>()
                    .GetViewByChassiAsync(Chassi);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ConsultarVeiculoPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<DetranRioVeiculoViewModel>> ConsultarVeiculoPorIdentificador(int IdentificadorVeiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DetranRioVeiculoViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DetranRioService>()
                    .GetViewByIdAsync(IdentificadorVeiculo);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ConsultarVeiculoPorPlaca")]
        // TODO: [Authorize]
        public async Task<ActionResult<DetranRioVeiculoViewModel>> ConsultarVeiculoPorPlaca(string Placa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DetranRioVeiculoViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DetranRioService>()
                    .GetViewByPlacaAsync(Placa);

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
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;
using WebZi.Plataform.Domain.ViewModel;

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

        [HttpGet("ConsultarVeiculoPorPlaca")]
        // TODO: [Authorize]
        public async Task<ActionResult<DetranRioVeiculoModel>> ConsultarVeiculoPorPlaca(string Placa)
        {
            MensagemViewModel ResultView = new();

            if (!ModelState.IsValid)
            {
                ResultView = MensagemViewHelper.SetBadRequest();

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }

            try
            {
                DetranRioVeiculoModel dsa = await _provider
                    .GetService<DetranRioService>()
                    .GetByPlacaAsync(Placa);

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
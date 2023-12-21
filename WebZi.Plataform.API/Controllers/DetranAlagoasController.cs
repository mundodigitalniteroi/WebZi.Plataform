using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Envio;
using WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Response;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetranAlagoasController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public DetranAlagoasController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("ConsultarVeiculoApreensao")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ResultViewModel>> ConsultarVeiculoApreensao([FromBody] AutorizarRetiradaModel AutorizarRetirada)
        {
            ResultViewModel ResultView = new();

            if (!ModelState.IsValid)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }

            try
            {
                ResultView = await _provider
                    .GetService<DetranAlagoasService>()
                    .ConsultarVeiculoApreensaoAsync(AutorizarRetirada);

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
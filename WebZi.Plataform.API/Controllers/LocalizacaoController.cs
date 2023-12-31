using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Domain.DTO.Localizacao;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public LocalizacaoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("SelecionarEnderecoCompleto")]
        // TODO: [Authorize]
        public ActionResult<EnderecoDTO> SelecionarEnderecoCompleto(string CEP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EnderecoDTO ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<EnderecoService>()
                    .GetByCEP(CEP);

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
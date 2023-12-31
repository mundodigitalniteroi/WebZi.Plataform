using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Pessoa;
using WebZi.Plataform.Domain.DTO.Pessoa;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public PessoaController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("ListarTipoDocumentoIdentificacao")]
        // TODO: [Authorize]
        public async Task<ActionResult<TipoDocumentoIdentificacaoListDTO>> ListarTipoDocumentoIdentificacao()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoDocumentoIdentificacaoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<PessoaService>()
                    .ListTipoDocumentoIdentificacaoAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoDocumentoIdentificacaoSimplificado")]
        // TODO: [Authorize]
        public async Task<ActionResult<TipoDocumentoIdentificacaoSimplificadoListDTO>> ListarTipoDocumentoIdentificacaoSimplificado()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoDocumentoIdentificacaoSimplificadoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<PessoaService>()
                    .ListTipoDocumentoIdentificacaoSimplificadoAsync();

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
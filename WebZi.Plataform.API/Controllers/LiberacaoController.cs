using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Liberacao;
using WebZi.Plataform.Domain.DTO.Report;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.Liberacao;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiberacaoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public LiberacaoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("EntregaSimplificada")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> EntregaSimplificada([FromBody] EntregaSimplificadaParameters Parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<LiberacaoService>()
                    .EntregaSimplificadaAsync(Parameters);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GuiaAutorizacaoRetiradaVeiculo")]
        // TODO: [Authorize]
        public async Task<ActionResult<GuiaAutorizacaoRetiradaVeiculoDTO>> GuiaAutorizacaoRetiradaVeiculo(int IdentificadorProcesso, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GuiaAutorizacaoRetiradaVeiculoDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<LiberacaoService>()
                    .CreateGuiaAutorizacaoRetiradaVeiculoAsync(IdentificadorProcesso, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ValidarGuiaAutorizacaoRetiradaVeiculo")]
        // TODO: [Authorize]
        public async Task<ActionResult<ValidacaoGuiaAutorizacaoRetiradaVeiculoDTO>> ValidarGuiaAutorizacaoRetiradaVeiculo(string Input, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ValidacaoGuiaAutorizacaoRetiradaVeiculoDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<LiberacaoService>()
                    .ValidarGuiaAutorizacaoRetiradaVeiculoAsync(Input, IdentificadorUsuario);

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
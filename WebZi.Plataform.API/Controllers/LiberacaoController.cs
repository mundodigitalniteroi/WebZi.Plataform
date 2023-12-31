using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Liberacao;
using WebZi.Plataform.Domain.DTO.Report;

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
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Liberacao;
using WebZi.Plataform.Domain.ViewModel.Report;

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
        public async Task<ActionResult<GuiaAutorizacaoRetiradaVeiculoViewModel>> GuiaAutorizacaoRetiradaVeiculo(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GuiaAutorizacaoRetiradaVeiculoViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<LiberacaoService>()
                    .CreateGuiaAutorizacaoRetiradaVeiculoAsync(IdentificadorGrv, IdentificadorUsuario);

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
        public async Task<ActionResult<ValidacaoGuiaAutorizacaoRetiradaVeiculoViewModel>> ValidarGuiaAutorizacaoRetiradaVeiculo(string Input, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ValidacaoGuiaAutorizacaoRetiradaVeiculoViewModel ResultView = new();

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
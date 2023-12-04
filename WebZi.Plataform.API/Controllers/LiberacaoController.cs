using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Liberacao;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Generic;

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
        public async Task<ActionResult<ImageViewModelList>> GuiaAutorizacaoRetiradaVeiculo(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<LiberacaoService>()
                    .CreateGuiaAutorizacaoRetiradaVeiculoAsync(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Domain.ViewModel.Faturamento;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicosController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public ServicosController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("ListarServicoAssociadoTipoVeiculo")]
        // TODO: [Authorize]
        public async Task<ActionResult<ServicoAssociadoTipoVeiculoViewModelList>> ListarServicoAssociadoTipoVeiculo(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServicoAssociadoTipoVeiculoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .ListarServicoAssociadoTipoVeiculoAsync(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Empresa;
using WebZi.Plataform.Domain.ViewModel.Empresa;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public EmpresaController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("Listar")]
        // TODO: [Authorize]
        public async Task<ActionResult<EmpresaViewModelList>> Listar(string CNPJ, string Nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpresaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<EmpresaService>()
                    .ListAsync(CNPJ, Nome);

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
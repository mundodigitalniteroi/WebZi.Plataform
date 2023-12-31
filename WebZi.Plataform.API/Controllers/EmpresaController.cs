using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Empresa;
using WebZi.Plataform.Domain.DTO.Empresa;

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
        public async Task<ActionResult<EmpresaListDTO>> Listar(string CNPJ, string Nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpresaListDTO ResultView = new();

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
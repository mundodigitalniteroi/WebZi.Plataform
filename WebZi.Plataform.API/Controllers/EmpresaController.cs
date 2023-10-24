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
        public async Task<ActionResult<EmpresaViewModelList>> Listar(string CNPJ, string Nome)
        {
            EmpresaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<EmpresaService>()
                    .List(CNPJ, Nome);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
    }
}
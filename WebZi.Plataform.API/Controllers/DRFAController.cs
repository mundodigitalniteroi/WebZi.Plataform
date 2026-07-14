using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.DRFA;
using WebZi.Plataform.Domain.DTO.DRFA;

namespace WebZi.Plataform.API.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DRFAController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public DRFAController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("SelecionarDRFAPorIdentificador")]
        public async Task<ActionResult<DRFADTO>> SelecionarDRFAPorIdentificador(int IdentificadorProcesso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            DRFADTO ResultView = new();
        
            try
            {
                ResultView = await _provider
                    .GetService<DRFAService>()
                    .GetDRFAAsync(IdentificadorProcesso);
        
                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);
        
                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
        
        [HttpGet("ListaArquivos")]
        public async Task<ActionResult<ListArquivosDRFADTO>> ListaArquivos(int IdentificadorProcesso, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ListArquivosDRFADTO ResultView = new();
            try
            {
                ResultView = await _provider
                    .GetService<DRFAService>()
                    .GetArquivos(IdentificadorProcesso, IdentificadorUsuario);
        
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

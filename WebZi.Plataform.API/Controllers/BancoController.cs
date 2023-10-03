using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Domain.ViewModel.Banco;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public BancoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<BancoViewModelList>> SelecionarPorId(short BancoId)
        {
            BancoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<BancoService>()
                    .GetById(BancoId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorNome")]
        public async Task<ActionResult<BancoViewModelList>> SelecionarPorNome(string Nome)
        {
            BancoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<BancoService>()
                    .GetByName(Nome);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<BancoViewModelList>> Listar()
        {
            BancoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<BancoService>()
                    .List();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarAgenciasBancarias")]
        public async Task<ActionResult<AgenciaBancariaViewModelList>> ListarAgenciasBancarias(short BancoId)
        {
            AgenciaBancariaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AgenciaBancariaService>()
                    .List(BancoId);

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
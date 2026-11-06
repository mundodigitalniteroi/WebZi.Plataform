using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Servico;
using WebZi.Plataform.Domain.DTO.Servico;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReboquistaController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public ReboquistaController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("ListarReboquista")]
        public async Task<ActionResult<ReboquistaListDTO>> ListarReboquista(int IdentificadorCliente,
            int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboquistaListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .ListReboquistaAsync(IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquistaPorIdentificador")]
        public async Task<ActionResult<ReboquistaListDTO>> SelecionarReboquistaPorIdentificador(
            int IdentificadorReboquista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboquistaListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboquistaByIdAsync(IdentificadorReboquista);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<MensagemDTO>> Cadastrar(CadastrarReboquistaParameters parameters)
        {
            MensagemDTO ResultView = new();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .CreateReboquistaAsync(parameters);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception e)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(e);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult<MensagemDTO>> Atualizar(AtualizarReboquistaParameters parameters)
        {
            MensagemDTO ResultView = new();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .UpdateReboquistaAsync(parameters);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception e)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(e);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }
    }
}
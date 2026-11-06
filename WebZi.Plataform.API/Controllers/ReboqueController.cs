using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Servico;
using WebZi.Plataform.Domain.DTO.Servico;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.Reboque;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReboqueController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public ReboqueController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("ListarReboque")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueListDTO>> ListarReboque(int IdentificadorCliente,
            int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .ListReboqueAsync(IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueListDTO>> SelecionarReboquePorIdentificador(int IdentificadorReboque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboqueByIdAsync(IdentificadorReboque);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorPlaca")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueListDTO>> SelecionarReboquePorPlaca(string Placa,
            int IdentificadorCliente, int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboqueByPlacaAsync(Placa, IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<MensagemDTO>> Cadastrar(CadastrarReboqueParameters parameters)
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
                    .CreateReboqueAsync(parameters);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception e)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(e);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult<MensagemDTO>> Atualizar(AtualizarReboqueParameters parameters)
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
                    .UpdateReboqueAsync(parameters);

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
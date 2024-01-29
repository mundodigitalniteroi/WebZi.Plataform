using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Domain.DTO.Atendimento;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.Atendimento;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public AtendimentoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("Cadastrar")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<AtendimentoCadastroDTO>> Cadastrar([FromBody] AtendimentoParameters Atendimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AtendimentoCadastroDTO ResultView = new();

            try
            {
                ResultView.Mensagem = await _provider
                    .GetService<AtendimentoService>()
                    .CheckInformacoesParaCadastroAsync(Atendimento);

                if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
                }
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .CreateAtendimentoAsync(Atendimento);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarFotoResponsavel")]
        // TODO: [Authorize]
        public async Task<ActionResult<ImageListDTO>> SelecionarFotoResponsavel(int IdentificadorAtendimento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetResponsavelFotoAsync(IdentificadorAtendimento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<AtendimentoDTO>> SelecionarPorIdentificador(int IdentificadorAtendimento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AtendimentoDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetByIdAsync(IdentificadorAtendimento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorProcesso")]
        // TODO: [Authorize]
        public async Task<ActionResult<AtendimentoDTO>> SelecionarPorProcesso(string NumeroProcesso, string CodigoProduto, int IdentificadorCliente, int IdentificadorDeposito, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AtendimentoDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetByProcessoAsync(NumeroProcesso, CodigoProduto, IdentificadorCliente, IdentificadorDeposito, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("ValidarInformacoesParaCadastro")]
        [IgnoreAntiforgeryToken]
        // TODO: [Authorize]
        public async Task<ActionResult<MensagemDTO>> ValidarInformacoesParaCadastro([FromBody] AtendimentoParameters Atendimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .CheckInformacoesParaCadastroAsync(Atendimento);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }
    }
}
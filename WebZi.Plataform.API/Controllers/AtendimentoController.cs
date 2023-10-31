using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Generic;

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
        public async Task<ActionResult<AtendimentoCadastroResultViewModel>> Cadastrar([FromBody] AtendimentoCadastroInputViewModel Atendimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AtendimentoCadastroResultViewModel ResultView = new();

            try
            {
                ResultView.Mensagem = await _provider
                    .GetService<AtendimentoService>()
                    .ValidarInformacoesParaCadastroAsync(Atendimento);

                if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView.Mensagem);
                }
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .Cadastrar(Atendimento);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView.Mensagem);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<AtendimentoViewModel>> SelecionarPorIdentificador(int IdentificadorAtendimento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AtendimentoViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetByIdAsync(IdentificadorAtendimento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorProcesso")]
        // TODO: [Authorize]
        public async Task<ActionResult<AtendimentoViewModel>> SelecionarPorProcesso(string NumeroProcesso, string CodigoProduto, int IdentificadorCliente, int IdentificadorDeposito, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AtendimentoViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetByProcessoAsync(NumeroProcesso, CodigoProduto, IdentificadorCliente, IdentificadorDeposito, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarFotoResponsavel")]
        // TODO: [Authorize]
        public async Task<ActionResult<ImageViewModelList>> SelecionarFotoResponsavel(int IdentificadorAtendimento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetResponsavelFotoAsync(IdentificadorAtendimento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("ValidarInformacoesParaCadastro")]
        [IgnoreAntiforgeryToken]
        // TODO: [Authorize]
        public async Task<ActionResult<MensagemViewModel>> ValidarInformacoesParaCadastro([FromBody] AtendimentoCadastroInputViewModel Atendimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .ValidarInformacoesParaCadastroAsync(Atendimento);

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
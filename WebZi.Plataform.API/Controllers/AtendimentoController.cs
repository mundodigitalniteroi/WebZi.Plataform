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
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<AtendimentoCadastroResultViewModel>> Cadastrar(AtendimentoCadastroInputViewModel Atendimento)
        {
            AtendimentoCadastroResultViewModel ResultView = new();

            try
            {
                ResultView.Mensagem = await _provider
                    .GetService<AtendimentoService>()
                    .ValidarInformacoesParaCadastro(Atendimento);

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

        [HttpGet("ListarQualificacaoResponsavel")]
        public async Task<ActionResult<QualificacaoResponsavelViewModelList>> ListarQualificacaoResponsavel()
        {
            QualificacaoResponsavelViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<QualificacaoResponsavelService>()
                    .ListAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorIdentificador")]
        public async Task<ActionResult<AtendimentoViewModel>> SelecionarPorIdentificador(int IdentificadorAtendimento, int IdentificadorUsuario)
        {
            AtendimentoViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetById(IdentificadorAtendimento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorProcesso")]
        public async Task<ActionResult<AtendimentoViewModel>> SelecionarPorProcesso(string NumeroProcesso, string CodigoProduto, int IdentificadorCliente, int IdentificadorDeposito, int IdentificadorUsuario)
        {
            AtendimentoViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetByProcesso(NumeroProcesso, CodigoProduto, IdentificadorCliente, IdentificadorDeposito, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarFotoResponsavel")]
        public async Task<ActionResult<ImageViewModelList>> SelecionarFotoResponsavel(int IdentificadorAtendimento, int IdentificadorUsuario)
        {
            ImageViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetResponsavelFoto(IdentificadorAtendimento, IdentificadorUsuario);

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
        public async Task<ActionResult<MensagemViewModel>> ValidarInformacoesParaCadastro(AtendimentoCadastroInputViewModel Atendimento)
        {
            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .ValidarInformacoesParaCadastro(Atendimento);

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
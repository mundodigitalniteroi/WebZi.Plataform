using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Domain.ViewModel;
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

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<AtendimentoViewModel>> SelecionarPorId(int AtendimentoId, int UsuarioId)
        {
            AtendimentoViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetById(AtendimentoId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError("Ocorreu um erro interno", ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorProcesso")]
        public async Task<ActionResult<AtendimentoViewModel>> SelecionarPorProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
        {
            AtendimentoViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetByProcesso(NumeroProcesso, ClienteId, DepositoId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError("Ocorreu um erro interno", ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarFotoResponsavel")]
        public async Task<ActionResult<AtendimentoFotoResponsavelViewModel>> SelecionarFotoResponsavel(int AtendimentoId, int UsuarioId)
        {
            AtendimentoFotoResponsavelViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .GetResponsavelFoto(AtendimentoId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError("Ocorreu um erro interno", ex);

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
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError("Ocorreu um erro interno", ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("ValidarInformacoesParaCadastro")]
        public async Task<ActionResult<MensagemViewModel>> ValidarInformacoesParaCadastro(AtendimentoCadastroViewModel Atendimento)
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
                ResultView = MensagemViewHelper.GetInternalServerError("Ocorreu um erro interno", ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<AtendimentoCadastroResultViewModel>> Cadastrar(AtendimentoCadastroViewModel Atendimento)
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
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError("Ocorreu um erro interno", ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .Cadastrar(Atendimento);

                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

                ResultView.Mensagem.AvisosInformativos.Add("Cadastro concluído com sucesso");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView.Mensagem);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError("Ocorreu um erro interno", ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
    }
}
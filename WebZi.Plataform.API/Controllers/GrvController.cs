using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Data.Services.Servico;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.Servico;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrvController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public GrvController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<GrvViewModel>> SelecionarPorId(int GrvId, int UsuarioId)
        {
            GrvViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetById(GrvId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorProcesso")]
        public async Task<ActionResult<GrvViewModelList>> SelecionarPorProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
        {
            GrvViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetByNumeroFormularioGrv(NumeroProcesso, ClienteId, DepositoId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorId")]
        public async Task<ActionResult<ReboqueViewModelList>> SelecionarReboquePorId(int ReboqueId)
        {
            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ReboqueService>()
                    .GetById(ReboqueId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorPlaca")]
        public async Task<ActionResult<ReboqueViewModelList>> SelecionarReboquePorPlaca(string Placa, int ClienteId, int DepositoId)
        {
            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ReboqueService>()
                    .GetByPlaca(Placa, ClienteId, DepositoId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarReboques")]
        public async Task<ActionResult<ReboqueViewModelList>> ListarReboques(int ClienteId, int DepositoId)
        {
            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ReboqueService>()
                    .List(ClienteId, DepositoId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquistaPorId")]
        public async Task<ActionResult<ReboquistaViewModelList>> SelecionarReboquistaPorId(int ReboquistaId)
        {
            ReboquistaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ReboquistaService>()
                    .GetById(ReboquistaId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarReboquistas")]
        public async Task<ActionResult<ReboquistaViewModelList>> ListarReboquistas(int ClienteId, int DepositoId)
        {
            ReboquistaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ReboquistaService>()
                    .List(ClienteId, DepositoId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarAutoridadesResponsaveis")]
        public async Task<ActionResult<AutoridadeResponsavelViewModelList>> ListarAutoridadesResponsaveis(string UF)
        {
            AutoridadeResponsavelViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AutoridadeResponsavelService>()
                    .List(UF);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarLacres")]
        public async Task<ActionResult<LacreViewModelList>> ListarLacres(int GrvId, int UsuarioId)
        {
            LacreViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<LacreService>()
                    .List(GrvId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarMotivosApreensoes")]
        public async Task<ActionResult<MotivoApreensaoViewModelList>> ListarMotivosApreensoes()
        {
            MotivoApreensaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<MotivoApreensaoService>()
                    .List();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarStatusOperacoes")]
        public async Task<ActionResult<StatusOperacaoViewModelList>> ListarStatusOperacoes()
        {
            StatusOperacaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<StatusOperacaoService>()
                    .List();

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
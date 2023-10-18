using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Domain.ViewModel.GGV;
using WebZi.Plataform.Domain.ViewModel.Veiculo;
using WebZi.Plataform.Domain.ViewModel.Vistoria;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GgvController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public GgvController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("ListarDadosMestres")]
        public async Task<ActionResult<DadosMestresViewModel>> ListarDadosMestres()
        {
            DadosMestresViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .ListarDadosMestres();

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                var error = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)error.HtmlStatusCode, error);
            }
        }

        [HttpGet("ListarEstadoGeralVeiculo")]
        public async Task<ActionResult<VistoriaEstadoGeralVeiculoViewModelList>> ListarEstadoGeralVeiculo()
        {
            VistoriaEstadoGeralVeiculoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VistoriaService>()
                    .ListarEstadoGeralVeiculo();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarSituacaoChassi")]
        public async Task<ActionResult<VistoriaSituacaoChassiViewModelList>> ListarSituacaoChassi()
        {
            VistoriaSituacaoChassiViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VistoriaService>()
                    .ListarSituacaoChassi();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarStatusVistoria")]
        public async Task<ActionResult<VistoriaStatusViewModelList>> ListarStatusVistoria()
        {
            VistoriaStatusViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VistoriaService>()
                    .ListarStatusVistoria();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoAvaria")]
        public async Task<ActionResult<TipoAvariaViewModelList>> ListarTipoAvaria()
        {
            TipoAvariaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TipoAvariaService>()
                    .ListarTipoAvaria();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoDirecao")]
        public async Task<ActionResult<VistoriaTipoDirecaoViewModelList>> ListarTipoDirecao()
        {
            VistoriaTipoDirecaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VistoriaService>()
                    .ListarTipoDirecao();

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
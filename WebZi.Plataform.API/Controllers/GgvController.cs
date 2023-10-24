using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Data.Services.Veiculo;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GGV;
using WebZi.Plataform.Domain.ViewModel.GRV;
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

        [HttpPost("CadastrarFotos")]
        [IgnoreAntiforgeryToken]
        public ActionResult<MensagemViewModel> CadastrarFotos(GrvFotoViewModel Fotos)
        {
            MensagemViewModel ResultView;

            try
            {
                ResultView = _provider
                    .GetService<GrvService>()
                    .SendFiles(Fotos);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarDadosMestres")]
        public async Task<ActionResult<DadosMestresViewModel>> ListarDadosMestres(byte IdentificadorTipoVeiculo)
        {
            DadosMestresViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .ListarDadosMestres(IdentificadorTipoVeiculo);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                var error = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)error.HtmlStatusCode, error);
            }
        }

        [HttpGet("ListarEquipamentoOpcional")]
        public async Task<ActionResult<EquipamentoOpcionalViewModelList>> ListarEquipamentoOpcional(byte IdentificadorTipoVeiculo)
        {
            EquipamentoOpcionalViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VeiculoService>()
                    .ListarEquipamentoOpcional(IdentificadorTipoVeiculo);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarFoto")]
        public async Task<ActionResult<ImageViewModelList>> ListarFoto(int IdentificadorGrv, int IdentificadorUsuario)
        {
            ImageViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .ListarFotos(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
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
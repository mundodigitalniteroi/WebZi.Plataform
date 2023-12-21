using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Report;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.Report;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturamentoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public FaturamentoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("AlterarFormaPagamento")]
        // TODO: [Authorize]
        public ActionResult<MensagemViewModel> AlterarFormaPagamento(int IdentificadorFaturamento, byte IdentificadorNovaFormaPagamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = _provider
                    .GetService<FaturamentoService>()
                    .UpdateFormaPagamento(IdentificadorFaturamento, IdentificadorNovaFormaPagamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarGuiaPagamentoReboqueEstadia")]
        // TODO: [Authorize]
        public async Task<ActionResult<GuiaPagamentoReboqueEstadiaViewModel>> GerarGuiaPagamentoReboqueEstadia(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GuiaPagamentoReboqueEstadiaViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GuiaPagamentoReboqueEstadiaService>()
                    .GetGuiaPagamentoReboqueEstadiaAsync(IdentificadorFaturamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarServicoAssociadoTipoVeiculo")]
        // TODO: [Authorize]
        public async Task<ActionResult<ServicoAssociadoTipoVeiculoViewModelList>> ListarServicoAssociadoTipoVeiculo(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServicoAssociadoTipoVeiculoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<FaturamentoService>()
                    .ListServicoAssociadoTipoVeiculoAsync(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarServicoAssociadoGrv")]
        // TODO: [Authorize]
        public async Task<ActionResult<ServicoAssociadoTipoVeiculoViewModelList>> ListarServicoAssociadoGrv(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServicoAssociadoTipoVeiculoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<FaturamentoService>()
                    .ListServicoAssociadoTipoVeiculoAsync(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoCobranca")]
        // TODO: [Authorize]
        public async Task<ActionResult<TabelaGenericaViewModelList>> ListarTipoCobranca()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TabelaGenericaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TabelaGenericaService>()
                    .ListToViewModelAsync("FAT_TIPO_COBRANCA");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("Simulacao")]
        // TODO: [Authorize]
        public async Task<ActionResult<TabelaGenericaViewModelList>> Simulacao()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TabelaGenericaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TabelaGenericaService>()
                    .ListToViewModelAsync("FAT_TIPO_COBRANCA");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
    }
}
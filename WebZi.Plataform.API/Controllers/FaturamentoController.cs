using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Banco.PIX;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Banco.PIX;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Generic;

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
                    .AlterarFormaPagamento(IdentificadorFaturamento, IdentificadorNovaFormaPagamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarBoleto")]
        // TODO: [Authorize]
        public ActionResult<ImageViewModelList> GerarBoleto(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageViewModelList ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<FaturamentoBoletoService>()
                    .Create(IdentificadorFaturamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarGuiaPagamentoReboqueEstadia")]
        // TODO: [Authorize]
        public async Task<ActionResult<GerarPagamentoReboqueEstadiaViewModel>> GerarGuiaPagamentoReboqueEstadia(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GerarPagamentoReboqueEstadiaViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<FaturamentoGuiaPagamentoReboqueEstadiaService>()
                    .GetGuiaPagamentoReboqueEstadiaAsync(IdentificadorFaturamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarPixDinamico")]
        // TODO: [Authorize]
        public ActionResult<PixEstaticoGeradoViewModel> GerarPixDinamico(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PixEstaticoGeradoViewModel ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<PixDinamicoService>()
                    .Create(IdentificadorFaturamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarPixEstatico")]
        // TODO: [Authorize]
        public ActionResult<PixEstaticoGeradoViewModel> GerarPixEstatico(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PixEstaticoGeradoViewModel ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<PixEstaticoService>()
                    .Create(IdentificadorFaturamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

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
                    .ListarServicoAssociadoTipoVeiculoAsync(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

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
                    .ListarServicoAssociadoTipoVeiculoAsync(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

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
                    .GetService<SistemaService>()
                    .ListarTabelaGenericaViewModelAsync("FAT_TIPO_COBRANCA");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarBoleto")]
        // TODO: [Authorize]
        public ActionResult<ImageViewModelList> SelecionarBoleto(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageViewModelList ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<FaturamentoBoletoService>()
                    .GetBoletoNaoCancelado(IdentificadorFaturamento, IdentificadorUsuario);

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
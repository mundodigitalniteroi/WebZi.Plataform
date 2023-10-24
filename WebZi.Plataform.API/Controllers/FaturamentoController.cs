using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Banco.PIX;
using WebZi.Plataform.Data.Services.Faturamento;
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
        public ActionResult<MensagemViewModel> AlterarFormaPagamento(int IdentificadorFaturamento, byte IdentificadorNovaFormaPagamento, int IdentificadorUsuario)
        {
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
        public ActionResult<ImageViewModelList> GerarBoleto(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
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
        public async Task<ActionResult<GerarPagamentoReboqueEstadiaViewModel>> GerarGuiaPagamentoReboqueEstadia(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            GerarPagamentoReboqueEstadiaViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<FaturamentoGuiaPagamentoReboqueEstadiaService>()
                    .GetGuiaPagamentoReboqueEstadia(IdentificadorFaturamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarPixEstatico")]
        public ActionResult<PixEstaticoGeradoViewModel> GerarPixEstatico(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
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

        [HttpGet("GerarPixDinamico")]
        public ActionResult<PixEstaticoGeradoViewModel> GerarPixDinamico(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
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

        [HttpGet("ListarTipoMeioCobranca")]
        public ActionResult<TipoMeioCobrancaViewModelList> ListarTipoMeioCobranca()
        {
            TipoMeioCobrancaViewModelList ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<TipoMeioCobrancaService>()
                    .List();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarBoleto")]
        public ActionResult<ImageViewModelList> SelecionarBoleto(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
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
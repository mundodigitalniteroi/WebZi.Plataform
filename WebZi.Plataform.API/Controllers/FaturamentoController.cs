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
        public ActionResult<MensagemViewModel> AlterarFormaPagamento(int FaturamentoId, byte NovaFormaPagamentoId, int UsuarioId)
        {
            MensagemViewModel ResultView;

            try
            {
                ResultView = _provider
                    .GetService<FaturamentoService>()
                    .AlterarFormaPagamento(FaturamentoId, NovaFormaPagamentoId, UsuarioId);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarBoleto")]
        public ActionResult<ImageViewModel> GerarBoleto(int FaturamentoId, int UsuarioId)
        {
            ImageViewModel ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<FaturamentoBoletoService>()
                    .Create(FaturamentoId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarPixEstatico")]
        public ActionResult<PixEstaticoGeradoViewModel> GerarPixEstatico(int FaturamentoId, int UsuarioId)
        {
            PixEstaticoGeradoViewModel ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<PixEstaticoService>()
                    .Create(FaturamentoId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarPixDinamico")]
        public ActionResult<PixEstaticoGeradoViewModel> GerarPixDinamico(int FaturamentoId, int UsuarioId)
        {
            PixEstaticoGeradoViewModel ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<PixDinamicoService>()
                    .Create(FaturamentoId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarGuiaPagamentoReboqueEstadia")]
        public ActionResult<GerarPagamentoReboqueEstadiaViewModel> GerarGuiaPagamentoReboqueEstadia(int FaturamentoId, int UsuarioId)
        {
            GerarPagamentoReboqueEstadiaViewModel ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<FaturamentoGuiaPagamentoReboqueEstadiaService>()
                    .GetGuiaPagamentoReboqueEstadia(FaturamentoId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoMeioCobranca")]
        public ActionResult<TipoMeioCobrancaViewModel> ListarTipoMeioCobranca()
        {
            TipoMeioCobrancaViewModel ResultView = new();

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
        public ActionResult<ImageViewModel> SelecionarBoleto(int FaturamentoId, int UsuarioId)
        {
            ImageViewModel ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<FaturamentoBoletoService>()
                    .GetBoletoNaoCancelado(FaturamentoId, UsuarioId);

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
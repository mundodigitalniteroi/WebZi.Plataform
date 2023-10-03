using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Cliente;
using WebZi.Plataform.Domain.ViewModel.Generic;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturamentoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IServiceProvider _provider;

        public FaturamentoController(AppDbContext context, IServiceProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        [HttpGet("ListarTipoMeioCobranca")]
        public async Task<ActionResult<List<TipoMeioCobrancaModel>>> ListarTipoMeioCobranca()
        {
            return Ok(await _provider
                .GetService<TipoMeioCobrancaService>()
                .List());
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
    }
}
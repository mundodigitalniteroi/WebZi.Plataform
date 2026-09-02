using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Domain.DTO.Faturamento;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.Pagamento;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public PagamentoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("ValidarInformacoesParaPagamento")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> ValidarInformacoesParaPagamento(
            [FromBody] PagamentoParameters Atendimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView = new();
            ResultView = await _provider
                .GetService<AtendimentoService>()
                .CheckInformacoesParaPagamentoAsync(Atendimento);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }

            return ResultView;
        }

        [HttpPost("ConfirmarPagamento")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<FaturamentoDTO>> ConfirmarPagamento([FromBody] PagamentoParameters model,
            CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FaturamentoDTO ResultView = new();
            ResultView = await _provider
                .GetService<FaturamentoService>()
                .ConfirmarPagamentoAsync(model, ct);
            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }

            return ResultView;
        }
    }
}
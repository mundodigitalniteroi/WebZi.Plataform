using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.Pagamento;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public PagamentoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("ValidarInformacoesParaPagamento")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> ValidarInformacoesParaPagamento([FromBody] PagamentoParameters Atendimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO mensagem = await _provider
                .GetService<AtendimentoService>()
                .CheckInformacoesParaPagamentoAsync(Atendimento);

            if (mensagem.Erros.Count == 0)
            {
                mensagem.HtmlStatusCode = CrossCutting.Web.HtmlStatusCodeEnum.Ok;

                return Ok(mensagem);
            }
            else
            {
                mensagem.HtmlStatusCode = CrossCutting.Web.HtmlStatusCodeEnum.BadRequest;

                return BadRequest(mensagem);
            }
        }
    }
}
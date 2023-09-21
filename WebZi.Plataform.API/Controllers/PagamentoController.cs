using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Domain.Models;
using WebZi.Plataform.Domain.Models.Pagamento.ViewModel;

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
        public async Task<ActionResult<MensagemViewModel>> ValidarInformacoesParaPagamento(PagamentoViewModel Atendimento)
        {
            MensagemViewModel mensagem = await _provider
                .GetService<AtendimentoService>()
                .ValidarInformacoesParaPagamento(Atendimento);

            if (mensagem.Erros.Count == 0)
            {
                mensagem.Status = "APTO PARA O PAGAMENTO";

                return Ok(mensagem);
            }
            else
            {
                mensagem.Status = "NÃO ESTÁ APTO PARA O PAGAMENTO";

                return BadRequest(mensagem);
            }
        }
    }
}
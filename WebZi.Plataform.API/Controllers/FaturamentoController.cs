using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;
using WebZi.Plataform.Domain.Models.Faturamento.ViewModel;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel;

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
        public async Task<ActionResult<byte[]>> GerarBoleto(int FaturamentoId, int UsuarioId)
        {
            if (FaturamentoId <= 0)
            {
                return BadRequest("Identificador do Faturamento inválido");
            }

            //if (!await _provider.GetService<UsuarioService>().IsUserActive(UsuarioId))
            //{
            //    return BadRequest("Usuário sem permissão de acesso ou inexistente");
            //}

            FaturamentoModel Faturamento = await _context.Faturamento
                .Include(i => i.Atendimento)
                .ThenInclude(t => t.Grv)
                .Where(w => w.FaturamentoId == FaturamentoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Faturamento == null)
            {
                return NotFound("Faturamento não encontrado");
            }
            else if (Faturamento.Status == "C")
            {
                return BadRequest("Esse Faturamento está cancelado");
            }
            else if (Faturamento.Status == "P")
            {
                return BadRequest("Esse Faturamento já foi pago");
            }

            TipoMeioCobrancaModel TipoMeioCobranca = await _context.TipoMeioCobranca
                .Where(w => w.TipoMeioCobrancaId == Faturamento.TipoMeioCobrancaId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (TipoMeioCobranca == null)
            {
                return BadRequest("Forma de Pagamento inexistente");
            }
            else if (TipoMeioCobranca.Alias != "BOL" && TipoMeioCobranca.Alias != "BOLESP")
            {
                return BadRequest($"Esse Faturamento está cadastrado com outra Forma de Pagamento: {TipoMeioCobranca.Descricao}");
            }

            byte[] result = _provider
                .GetService<FaturamentoBoletoService>()
                .Gerar(Faturamento.Atendimento.Grv, Faturamento, UsuarioId, null, null);

            return result != null ? Ok(result) : NotFound("Não foi possível gerar o Boleto");
        }

        [HttpGet("SelecionarBoleto")]
        public ActionResult<BoletoResultView> SelecionarBoleto(int FaturamentoId, int UsuarioId)
        {
            BoletoResultView result = _provider
                .GetService<FaturamentoBoletoService>()
                .GetUltimoBoleto(FaturamentoId, UsuarioId);

            return StatusCode((int)result.Mensagem.HtmlStatusCode, result);
        }

        [HttpGet("AlterarFormaPagamento")]
        public ActionResult<MensagemViewModel> AlterarFormaPagamento(int FaturamentoId, byte NovaFormaPagamentoId, int UsuarioId)
        {
            MensagemViewModel result = _provider
                .GetService<FaturamentoService>()
                .AlterarFormaPagamento(FaturamentoId, NovaFormaPagamentoId, UsuarioId);

            return StatusCode(((int)result.HtmlStatusCode), result);
        }
    }
}
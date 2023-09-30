using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;
using WebZi.Plataform.Domain.Services.Usuario;

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

            if (!await _provider.GetService<UsuarioService>().IsUserActive(UsuarioId))
            {
                return BadRequest("Usuário sem permissão de acesso ou inexistente");
            }

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
        public async Task<ActionResult<byte[]>> SelecionarBoleto(int FaturamentoId, int UsuarioId)
        {
            if (FaturamentoId <= 0)
            {
                return BadRequest("Identificador do Faturamento inválido");
            }

            if (!await _provider.GetService<UsuarioService>().IsUserActive(UsuarioId))
            {
                return BadRequest("Usuário sem permissão de acesso ou inexistente");
            }

            FaturamentoModel Faturamento = await _context.Faturamento
                .Include(i => i.Atendimento)
                .ThenInclude(t => t.Grv)
                .Include(i => i.FaturamentoBoletos.Where(w => w.Status != "C"))
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

            if (Faturamento.FaturamentoBoletos?.Count == null)
            {
                return NotFound("Boleto não encontrado");
            }

            var result = await _provider
                .GetService<FaturamentoBoletoService>()
                .GetUltimoBoleto(Faturamento.FaturamentoBoletos.FirstOrDefault().FaturamentoBoletoId);

            return result != null ? Ok(result) : NotFound("Boleto foi cancelado ou inexistente");
        }

        [HttpGet("AlterarFormaPagamento")]
        public async Task<ActionResult<string>> AlterarFormaPagamento(int FaturamentoId, byte NovaFormaPagamentoId, int UsuarioId)
        {
            StringBuilder erros = new();

            if (FaturamentoId <= 0)
            {
                erros.AppendLine("Identificador do Faturamento inválido");
            }

            if (NovaFormaPagamentoId <= 0)
            {
                erros.AppendLine("Identificador da Forma de Pagamento inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.AppendLine("Identificador do Usuário inválido");
            }

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return BadRequest(erros.ToString());
            }

            if (!await _provider.GetService<UsuarioService>().IsUserActive(UsuarioId))
            {
                return BadRequest("Usuário sem permissão de acesso ou inexistente");
            }

            if (FaturamentoId <= 0)
            {
                return BadRequest("Identificador do Faturamento inválido");
            }

            FaturamentoModel Faturamento = await _context.Faturamento
                .Include(i => i.TipoMeioCobranca)
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

            if (Faturamento.TipoMeioCobrancaId == NovaFormaPagamentoId)
            {
                return BadRequest("Forma de Pagamento já selecionado");
            }

            TipoMeioCobrancaModel TipoMeioCobranca = await _context.TipoMeioCobranca
                .Where(w => w.TipoMeioCobrancaId == NovaFormaPagamentoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (TipoMeioCobranca == null)
            {
                return BadRequest("Forma de Pagamento inexistente");
            }

            bool result = _provider
                .GetService<FaturamentoService>()
                .AlterarFormaPagamento(FaturamentoId, NovaFormaPagamentoId, TipoMeioCobranca);

            return result ? Ok($"Forma de Pagamento alterada com sucesso: {TipoMeioCobranca.Descricao}") : BadRequest($"Não foi possível alterar a Forma de Pagamento");
        }
    }
}
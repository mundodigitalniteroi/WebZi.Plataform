using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Faturamento;

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

        [HttpGet("CreateBoleto")]
        public async Task<ActionResult<byte[]>> CreateBoleto(int FaturamentoId, int UsuarioId)
        {
            if (FaturamentoId <= 0)
            {
                return BadRequest("Identificador do Faturamento inválido");
            }

            FaturamentoModel Faturamento = await _context.Faturamentos
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

            byte[] result = _provider
                .GetService<FaturamentoBoletoService>()
                .Gerar(Faturamento.Atendimento.Grv, Faturamento, UsuarioId, null, null);

            return result != null ? Ok(result) : NotFound("Faturamento não encontrado");
        }
    }
}
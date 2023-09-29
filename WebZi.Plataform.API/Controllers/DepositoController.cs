using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Domain.Models.Deposito;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public DepositoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<List<object>>> Listar()
        {
            return Ok(await _provider
                .GetService<DepositoService>()
                .List());
        }

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<object>> SelecionarPorId(int DepositoId)
        {
            if (DepositoId <= 0)
            {
                return BadRequest("Identificador do Depósito inválido");
            }

            DepositoModel result = await _provider
                .GetService<DepositoService>()
                .GetById(DepositoId);

            if (result == null)
            {
                return NotFound("Depósito não encontrado");
            }

            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet("SelecionarPorNome")]
        public async Task<ActionResult<object>> SelecionarPorNome(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Primeiro é necessário informar o Nome do Depósito");
            }

            DepositoModel result = await _provider
                .GetService<DepositoService>()
                .GetByName(Name.ToUpper().Trim());

            if (result == null)
            {
                return NotFound("Depósito não encontrado");
            }

            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet("GetDateTimeById")]
        public async Task<ActionResult<DateTime>> GetDateTimeById(int DepositoId)
        {
            if (DepositoId <= 0)
            {
                return BadRequest("Identificador do Depósito inválido");
            }

            DateTime result = await _provider
                .GetService<DepositoService>()
                .GetDataHoraPorDeposito(DepositoId);

            return Ok(result);
        }
    }
}
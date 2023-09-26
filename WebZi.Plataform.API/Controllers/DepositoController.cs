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

        [HttpGet("List")]
        public async Task<ActionResult<List<object>>> List()
        {
            return Ok(await _provider
                .GetService<DepositoService>()
                .List());
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<object>> GetById(int DepositoId)
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

        [HttpGet("GetByName")]
        public async Task<ActionResult<object>> GetByName(string Name)
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
                .SelecionarDataHoraPorDeposito(DepositoId);

            return Ok(result);
        }
    }
}
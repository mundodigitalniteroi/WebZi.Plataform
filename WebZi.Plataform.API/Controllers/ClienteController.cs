using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Domain.Models.Cliente;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public ClienteController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("List")]
        public async Task<ActionResult<List<object>>> List()
        {
            return Ok(await _provider
                .GetService<ClienteService>()
                .List());
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<object>> GetById(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("Identificador do Cliente inválido");
            }

            ClienteModel result = await _provider
                .GetService<ClienteService>()
                .GetById(Id);

            if (result == null)
            {
                return NotFound("Cliente não encontrado");
            }

            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet("GetByName")]
        public async Task<ActionResult<object>> GetByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Primeiro é necessário informar o Nome do Cliente");
            }

            ClienteModel result = await _provider
                .GetService<ClienteService>()
                .GetByName(Name.ToUpper().Trim());

            if (result == null)
            {
                return NotFound("Cliente não encontrado");
            }

            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
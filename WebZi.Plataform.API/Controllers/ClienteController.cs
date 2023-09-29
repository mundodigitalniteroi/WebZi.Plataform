using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Domain.Models.Banco.ViewModel;
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

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<ClienteModel>> SelecionarPorId(int ClienteId)
        {
            if (ClienteId <= 0)
            {
                return BadRequest("Identificador do Cliente inválido");
            }

            ClienteModel result = await _provider
                .GetService<ClienteService>()
                .GetById(ClienteId);

            return result != null ? Ok(JsonConvert.SerializeObject(result)) : NotFound("Cliente não encontrado");
        }

        [HttpGet("SelecionarPorNome")]
        public async Task<ActionResult<ClienteModel>> SelecionarPorNome(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Primeiro é necessário informar o Nome do Cliente");
            }

            ClienteModel result = await _provider
                .GetService<ClienteService>()
                .GetByName(Name.ToUpper().Trim());

            return result != null ? Ok(JsonConvert.SerializeObject(result)) : NotFound("Cliente não encontrado");
        }
    }
}
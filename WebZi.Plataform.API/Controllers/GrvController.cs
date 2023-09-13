using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Services.GRV;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrvController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public GrvController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Informe o ID do GRV");
            }

            GrvModel grv = await _provider
                .GetService<GrvService>()
                .GetById(id);

            if (grv == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(grv));
        }

        [HttpGet()]
        public async Task<ActionResult<object>> GetByProcesso(string numeroFormulario, int clienteId, int depositoId)
        {
            StringBuilder erros = new();

            if (string.IsNullOrWhiteSpace(numeroFormulario))
            {
                erros.AppendLine("Informe o Número do Formulário");
            }

            if (clienteId <= 0)
            {
                erros.AppendLine("Informe o ID do Cliente");
            }

            if (depositoId <= 0)
            {
                erros.AppendLine("Informe o ID do Depósito");
            }

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return BadRequest(erros.ToString());
            }

            GrvModel grv = await _provider
                .GetService<GrvService>()
                .GetByProcesso(numeroFormulario, clienteId, depositoId);

            if (grv == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(grv));
        }

        // POST api/<GrvController>
        //[HttpPost]
        //public void Post(Grv grv)
        //{
        //}

        // PUT api/<GrvController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, Grv grv)
        //{
        //}
    }
}
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public AtendimentoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Informe o ID do Atendimento");
            }

            AtendimentoModel Atendimento = await _provider
                .GetService<AtendimentoService>()
                .GetById(id);

            if (Atendimento == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(Atendimento));
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

            AtendimentoModel Atendimento = await _provider
                .GetService<AtendimentoService>()
                .GetByProcesso(numeroFormulario, clienteId, depositoId);

            if (Atendimento == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(Atendimento));
        }

        [HttpGet()]
        public async Task<ActionResult<string>> ChecarGrvAptoParaCadastro(AtendimentoModel atendimento)
        {
            string erros = await _provider
                .GetService<AtendimentoService>()
                .ChecarGrvParaCadastro(atendimento);

            if (!string.IsNullOrWhiteSpace(erros))
            {
                return BadRequest(erros);
            }
            else
            {
                return "Todos os campos preenchidos corretamente";
            }
        }

        // POST api/<AtendimentoController>
        //[HttpPost]
        //public void Post(Atendimento atendimento)
        //{

        //}

        // PUT api/<AtendimentoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, Atendimento Atendimento)
        //{
        //}
    }
}
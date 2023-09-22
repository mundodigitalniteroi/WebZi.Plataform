using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.GRV.ViewModel;
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

        [HttpGet("{Identificador}/{Usuario}")]
        public async Task<ActionResult<object>> Get(int Identificador, int Usuario)
        {
            StringBuilder erros = new();

            if (Identificador <= 0)
            {
                erros.AppendLine("Identificador do GRV inválido");
            }

            if (Usuario <= 0)
            {
                erros.AppendLine("Identificador do Usuário inválido");
            }

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return BadRequest(erros.ToString());
            }

            GrvViewModel grv = await _provider
                .GetService<GrvService>()
                .GetById(Identificador, Usuario);

            if (grv == null)
            {
                return NotFound("GRV sem permissão de acesso ou inexistente");
            }

            return Ok(JsonConvert.SerializeObject(grv));
        }

        [HttpGet("{NumeroProcesso}/{Cliente}/{Deposito}/{Usuario}")]
        public async Task<ActionResult<object>> Get(string NumeroProcesso, int Cliente, int Deposito, int Usuario)
        {
            StringBuilder erros = new();

            if (string.IsNullOrWhiteSpace(NumeroProcesso))
            {
                erros.AppendLine("Informe o Número do Processo");
            }

            if (Cliente <= 0)
            {
                erros.AppendLine("Identificador do Cliente inválido");
            }

            if (Deposito <= 0)
            {
                erros.AppendLine("Identificador do Depósito inválido ");
            }

            if (Usuario <= 0)
            {
                erros.AppendLine("Identificador do Usuário inválido");
            }

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return BadRequest(erros.ToString());
            }

            GrvViewModel grv = await _provider
                .GetService<GrvService>()
                .GetByNumeroFormularioGrv(NumeroProcesso, Cliente, Deposito, Usuario);

            if (grv == null)
            {
                return NotFound("GRV sem permissão de acesso ou inexistente");
            }

            return Ok(JsonConvert.SerializeObject(grv));
        }

        [HttpGet("ListarStatusOperacao")]
        public async Task<ActionResult<List<StatusOperacaoModel>>> ListarStatusOperacao()
        {
            return Ok(await _provider
                .GetService<StatusOperacaoService>()
                .List());
        }

        [HttpGet("ListarLacres")]
        public async Task<ActionResult<List<LacreModel>>> ListarLacres(int GrvId, int Usuario)
        {
            return Ok(await _provider
                .GetService<LacreService>()
                .List(GrvId, Usuario));
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
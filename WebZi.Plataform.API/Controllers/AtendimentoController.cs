using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Faturamento;

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

        [HttpGet("{Identificador}/{Usuario}")]
        public async Task<ActionResult<object>> Get(int Identificador, int Usuario)
        {
            StringBuilder erros = new();

            if (Identificador <= 0)
            {
                erros.AppendLine("Identificador do Atendimento inválido");
            }

            if (Usuario <= 0)
            {
                erros.AppendLine("Identificador do Usuário inválido");
            }

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return BadRequest(erros.ToString());
            }

            AtendimentoModel atendimento = await _provider
                .GetService<AtendimentoService>()
                .GetById(Identificador, Usuario);

            if (atendimento == null)
            {
                return NotFound("Atendimento sem permissão de acesso ou inexistente");
            }

            return Ok(JsonConvert.SerializeObject(atendimento));
        }

        [HttpGet("{NumeroProcesso}/{Cliente}/{Deposito}/{Usuario}")]
        public async Task<ActionResult<object>> Get(string NumeroProcesso, int Cliente, int Deposito, int Usuario)
        {
            StringBuilder erros = new();

            if (string.IsNullOrWhiteSpace(NumeroProcesso))
            {
                erros.AppendLine("Informe o Número do Processo");
            }
            else if (!StringHelper.IsNumber(NumeroProcesso))
            {
                erros.AppendLine("Número do Processo inválido");
            }
            else if (Convert.ToInt64(NumeroProcesso) <= 0)
            {
                erros.AppendLine("Número do Processo inválido");
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

            AtendimentoModel atendimento = await _provider
                .GetService<AtendimentoService>()
                .GetByProcesso(NumeroProcesso, Cliente, Deposito, Usuario);

            if (atendimento == null)
            {
                return NotFound("Atendimento sem permissão de acesso ou inexistente");
            }

            return Ok(JsonConvert.SerializeObject(atendimento));
        }

        [HttpGet("QualificacaoResponsavel")]
        public async Task<ActionResult<List<QualificacaoResponsavelModel>>> ListarQualificacaoResponsavel()
        {
            return Ok(await _provider
                .GetService<QualificacaoResponsavelService>()
                .List());
        }

        [HttpGet("TipoMeioCobranca")]
        public async Task<ActionResult<List<TipoMeioCobrancaModel>>> ListarTipoMeioCobranca()
        {
            return Ok(await _provider
                .GetService<TipoMeioCobrancaService>()
                .List());
        }

        [HttpPost("ValidarInformacoesParaCadastro")]
        public async Task<ActionResult<AtendimentoAvisoViewModel>> ValidarInformacoesParaCadastro(AtendimentoViewModel Atendimento)
        {
            AtendimentoAvisoViewModel aviso = await _provider
                .GetService<AtendimentoService>()
                .ValidarInformacoesParaCadastro(Atendimento);

            if (aviso.Erros.Count == 0)
            {
                aviso.Status = "APTO PARA O CADASTRO";

                return Ok(aviso);
            }
            else
            {
                aviso.Status = "NÃO ESTÁ APTO PARA O CADASTRO";

                return BadRequest(aviso);
            }
        }

        [HttpPost("ValidarInformacoesParaAtualizacao")]
        public async Task<ActionResult<AtendimentoAvisoViewModel>> ValidarInformacoesParaAtualizacao(AtendimentoViewModel Atendimento)
        {
            AtendimentoAvisoViewModel aviso = await _provider
                .GetService<AtendimentoService>()
                .ValidarInformacoesParaAtualizacao(Atendimento);

            if (aviso.Erros.Count == 0)
            {
                aviso.Status = "APTO PARA O CADASTRO";

                return Ok(aviso);
            }
            else
            {
                aviso.Status = "NÃO ESTÁ APTO PARA A ATUALIZAÇÃO";

                return BadRequest(aviso);
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<string>> Cadastrar(AtendimentoViewModel Atendimento)
        {
            AtendimentoAvisoViewModel aviso = await _provider
                .GetService<AtendimentoService>()
                .ValidarInformacoesParaCadastro(Atendimento);

            if (aviso.Erros.Count > 0)
            {
                aviso.Status = "NÃO ESTÁ APTO PARA O CADASTRO";

                return BadRequest(aviso);
            }

            try
            {
                string result = await _provider
                    .GetService<AtendimentoService>()
                    .Cadastrar(Atendimento);

                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AtendimentoController>/5
        //[HttpPut("{id}")]
        //public void Atualizar(int id, Atendimento Atendimento)
        //{
        //}
    }
}
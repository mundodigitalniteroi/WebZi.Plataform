using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Domain.Models;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Atendimento.ViewModel;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Usuario.ViewModel;
using WebZi.Plataform.Domain.Services.Usuario;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        private readonly IServiceProvider _provider;
        private readonly IMapper _mapper;

        public AtendimentoController(IServiceProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<AtendimentoViewModel>> GetById(int AtendimentoId, int UsuarioId)
        {
            StringBuilder erros = new();

            if (AtendimentoId <= 0)
            {
                erros.AppendLine("Identificador do Atendimento inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.AppendLine("Identificador do Usuário inválido");
            }

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return BadRequest(erros.ToString());
            }

            if (!await FindUser(UsuarioId))
            {
                return BadRequest("Usuário sem permissão de acesso ou inexistente");
            }

            AtendimentoModel result = await _provider
                .GetService<AtendimentoService>()
                .GetById(AtendimentoId, UsuarioId);

            return result != null ? _mapper.Map<AtendimentoViewModel>(result) : NotFound("Atendimento sem permissão de acesso ou inexistente");
        }

        [HttpGet("GetByProcesso")]
        public async Task<ActionResult<AtendimentoViewModel>> GetByProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
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

            if (ClienteId <= 0)
            {
                erros.AppendLine("Identificador do Cliente inválido");
            }

            if (DepositoId <= 0)
            {
                erros.AppendLine("Identificador do Depósito inválido ");
            }

            if (UsuarioId <= 0)
            {
                erros.AppendLine("Identificador do Usuário inválido");
            }

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return BadRequest(erros.ToString());
            }

            if (!await FindUser(UsuarioId))
            {
                return BadRequest("Usuário sem permissão de acesso ou inexistente");
            }

            AtendimentoModel result = await _provider
                .GetService<AtendimentoService>()
                .GetByProcesso(NumeroProcesso, ClienteId, DepositoId, UsuarioId);

            return result != null ? _mapper.Map<AtendimentoViewModel>(result) : NotFound("Atendimento sem permissão de acesso ou inexistente");
        }

        [HttpGet("GetBoleto")]
        public async Task<ActionResult<AtendimentoViewModel>> GetBoleto(int AtendimentoId, int UsuarioId)
        {
            StringBuilder erros = new();

            if (AtendimentoId <= 0)
            {
                erros.AppendLine("Identificador do Atendimento inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.AppendLine("Identificador do Usuário inválido");
            }

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return BadRequest(erros.ToString());
            }

            if (!await FindUser(UsuarioId))
            {
                return BadRequest("Usuário sem permissão de acesso ou inexistente");
            }

            AtendimentoModel result = await _provider
                .GetService<AtendimentoService>()
                .GetById(AtendimentoId, UsuarioId);

            return result != null ? _mapper.Map<AtendimentoViewModel>(result) : NotFound("Atendimento sem permissão de acesso ou inexistente");
        }

        [HttpGet("ListQualificacaoResponsavel")]
        public async Task<ActionResult<List<QualificacaoResponsavelModel>>> ListQualificacaoResponsavel()
        {
            var result = await _provider
                .GetService<QualificacaoResponsavelService>()
                .List();

            return result?.Count > 0 ? _mapper.Map<List<QualificacaoResponsavelModel>>(result) : NotFound("Qualificação do Responsável não encontrado");
        }

        [HttpGet("ListTipoMeioCobranca")]
        public async Task<ActionResult<List<TipoMeioCobrancaModel>>> ListTipoMeioCobranca()
        {
            return Ok(await _provider
                .GetService<TipoMeioCobrancaService>()
                .List());
        }

        [HttpPost("ValidarInformacoesParaCadastro")]
        public async Task<ActionResult<MensagemViewModel>> ValidarInformacoesParaCadastro(AtendimentoCadastroViewModel Atendimento)
        {
            MensagemViewModel mensagem = await _provider
                .GetService<AtendimentoService>()
                .ValidarInformacoesParaCadastro(Atendimento);

            if (mensagem.Erros.Count == 0)
            {
                mensagem.Status = "APTO PARA O CADASTRO";

                return Ok(mensagem);
            }
            else
            {
                mensagem.Status = "NÃO ESTÁ APTO PARA O CADASTRO";

                return BadRequest(mensagem);
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<AtendimentoCadastroResultViewModel>> Cadastrar(AtendimentoCadastroViewModel Atendimento)
        {
            MensagemViewModel mensagem = await _provider
                .GetService<AtendimentoService>()
                .ValidarInformacoesParaCadastro(Atendimento);

            if (mensagem.Erros.Count > 0)
            {
                mensagem.Status = "NÃO ESTÁ APTO PARA O CADASTRO";

                return BadRequest(mensagem);
            }

            if (!await FindUser(Atendimento.UsuarioId))
            {
                return BadRequest("Usuário sem permissão de acesso ou inexistente");
            }

            AtendimentoCadastroResultViewModel AtendimentoCadastroResultView = new();

            try
            {
                AtendimentoCadastroResultView = await _provider
                    .GetService<AtendimentoService>()
                    .Cadastrar(Atendimento);

                    AtendimentoCadastroResultView.Mensagem.Status = "CADASTRO CONCLUÍDO COM SUCESSO";

                    return Ok(AtendimentoCadastroResultView);
            }
            catch (Exception ex)
            {
                AtendimentoCadastroResultView.Mensagem.Status = "OCORREU UM ERRO AO CADASTRAR";

                AtendimentoCadastroResultView.Mensagem.Erros.Add(ex.Message);
                AtendimentoCadastroResultView.Mensagem.Erros.Add(ex.InnerException.Message);

                return BadRequest(AtendimentoCadastroResultView);
            }
        }

        private async Task<bool> FindUser(int UsuarioId)
        {
            UsuarioViewModel Usuario = await _provider
                .GetService<UsuarioService>()
                .GetById(UsuarioId);

            return Usuario != null && Usuario.FlagAtivo != "N";
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Atendimento.ViewModel;
using WebZi.Plataform.Domain.Models.Faturamento.ViewModel;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;

        public AtendimentoController(AppDbContext context, IMapper mapper, IServiceProvider provider)
        {
            _context = context;
            _mapper = mapper;
            _provider = provider;
        }

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<AtendimentoViewModel>> SelecionarPorId(int AtendimentoId, int UsuarioId)
        {
            AtendimentoViewModel AtendimentoView = new();

            List<string> erros = new();

            if (AtendimentoId <= 0)
            {
                erros.Add("Identificador do Atendimento inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
            }

            if (erros.Count > 0)
            {
                AtendimentoView.Mensagem = MensagemViewHelper.GetNewMessage(erros, MensagemTipoAvisoEnum.Impeditivo);

                return AtendimentoView;
            }

            if (!new UsuarioService(_context, _mapper).IsUserActive(UsuarioId))
            {
                AtendimentoView.Mensagem = MensagemViewHelper.GetNewMessage("Usuário sem permissão de acesso ou inexistente", MensagemTipoAvisoEnum.Impeditivo, HtmlStatusCodeEnum.Unauthorized);

                return AtendimentoView;
            }

            AtendimentoViewModel result = await _provider
                .GetService<AtendimentoService>()
                .GetById(AtendimentoId, UsuarioId);

            if (result != null)
            {
                AtendimentoView = _mapper.Map<AtendimentoViewModel>(result);

                AtendimentoView.Mensagem = MensagemViewHelper.GetOkMessage("OK");
            }
            else
            {
                AtendimentoView.Mensagem = MensagemViewHelper.GetNotFound("Atendimento sem permissão de acesso ou inexistente");

                return AtendimentoView;
            }

            return AtendimentoView;
        }

        [HttpGet("SelecionarPorProcesso")]
        public async Task<ActionResult<AtendimentoViewModel>> SelecionarPorProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
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

            //if (!await _provider.GetService<UsuarioService>().IsUserActive(UsuarioId))
            //{
            //    return BadRequest("Usuário sem permissão de acesso ou inexistente");
            //}

            AtendimentoModel result = await _provider
                .GetService<AtendimentoService>()
                .GetByProcesso(NumeroProcesso, ClienteId, DepositoId, UsuarioId);

            return result != null ? _mapper.Map<AtendimentoViewModel>(result) : NotFound("Atendimento sem permissão de acesso ou inexistente");
        }

        [HttpGet("SelecionarFotoResponsavel")]
        public async Task<ActionResult<byte[]>> SelecionarFotoResponsavel(int AtendimentoId, int UsuarioId)
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

            //if (!await _provider.GetService<UsuarioService>().IsUserActive(UsuarioId))
            //{
            //    return BadRequest("Usuário sem permissão de acesso ou inexistente");
            //}

            byte[] result = await _provider
                .GetService<AtendimentoService>()
                .GetResponsavelFoto(AtendimentoId);

            return result != null ? Ok(result) : NotFound("Este Atendimento não possui Foto do Responsável");
        }

        [HttpGet("ListarQualificacaoResponsavel")]
        public async Task<ActionResult<List<QualificacaoResponsavelModel>>> ListarQualificacaoResponsavel()
        {
            var result = await _provider
                .GetService<QualificacaoResponsavelService>()
                .List();

            return result?.Count > 0 ? _mapper.Map<List<QualificacaoResponsavelModel>>(result) : NotFound("Qualificação do Responsável não encontrado");
        }

        [HttpPost("ValidarInformacoesParaCadastro")]
        public async Task<ActionResult<MensagemViewModel>> ValidarInformacoesParaCadastro(AtendimentoCadastroViewModel Atendimento)
        {
            MensagemViewModel mensagem = await _provider
                .GetService<AtendimentoService>()
                .ValidarInformacoesParaCadastro(Atendimento);

            if (mensagem.Erros.Count == 0 && mensagem.AvisosImpeditivos.Count == 0)
            {
                mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

                return Ok(mensagem);
            }
            else
            {
                mensagem.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;

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
                mensagem.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;

                return BadRequest(mensagem);
            }

            AtendimentoCadastroResultViewModel AtendimentoCadastroResultView = new();

            try
            {
                AtendimentoCadastroResultView = await _provider
                    .GetService<AtendimentoService>()
                    .Cadastrar(Atendimento);

                AtendimentoCadastroResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

                return Ok(AtendimentoCadastroResultView);
            }
            catch (Exception ex)
            {
                AtendimentoCadastroResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.InternalServerError;

                AtendimentoCadastroResultView.Mensagem.Erros.Add(ex.Message);

                if (ex.InnerException != null)
                {
                    AtendimentoCadastroResultView.Mensagem.Erros.Add(ex.InnerException.Message);
                }

                return BadRequest(AtendimentoCadastroResultView);
            }
        }
    }
}
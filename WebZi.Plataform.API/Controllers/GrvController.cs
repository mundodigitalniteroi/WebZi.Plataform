using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.GRV.ViewModel;
using WebZi.Plataform.Domain.Models.Usuario.ViewModel;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrvController : ControllerBase
    {
        private readonly IServiceProvider _provider;
        private readonly IMapper _mapper;

        public GrvController(IServiceProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<GrvViewModel>> GetById(int GrvId, int UsuarioId)
        {
            StringBuilder erros = new();

            if (GrvId <= 0)
            {
                erros.AppendLine("Identificador do GRV inválido");
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

            GrvModel result = await _provider
                .GetService<GrvService>()
                .GetById(GrvId, UsuarioId);

            return result != null ? _mapper.Map<GrvViewModel>(result) : NotFound("GRV sem permissão de acesso ou inexistente");
        }

        [HttpGet("GetByProcesso")]
        public async Task<ActionResult<GrvViewModel>> GetByProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
        {
            StringBuilder erros = new();

            if (string.IsNullOrWhiteSpace(NumeroProcesso))
            {
                erros.AppendLine("Informe o Número do Processo");
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

            ClienteModel Cliente = await _provider
                .GetService<ClienteService>()
                .GetById(ClienteId);

            if (Cliente == null)
            {
                return NotFound("Cliente inexistente");
            }

            DepositoModel Deposito = await _provider
                .GetService<DepositoService>()
                .GetById(DepositoId);

            if (Deposito == null)
            {
                return NotFound("Depósito inexistente");
            }

            GrvModel result = await _provider
                .GetService<GrvService>()
                .GetByNumeroFormularioGrv(NumeroProcesso, ClienteId, DepositoId, UsuarioId);

            return result != null ? _mapper.Map<GrvViewModel>(result) : NotFound("GRV sem permissão de acesso ou inexistente");
        }

        [HttpGet("ListStatusOperacao")]
        public async Task<ActionResult<List<StatusOperacaoModel>>> ListStatusOperacao()
        {
            List<StatusOperacaoModel> result = await _provider
                .GetService<StatusOperacaoService>()
                .List();

            return result?.Count > 0 ? Ok(result) : NotFound("Status Operação não encontrado");
        }

        [HttpGet("ListLacres")]
        public async Task<ActionResult<List<LacreModel>>> ListLacres(int GrvId, int UsuarioId)
        {
            if (!await FindUser(UsuarioId))
            {
                return BadRequest("Usuário sem permissão de acesso ou inexistente");
            }

            List<LacreModel> result = await _provider
                .GetService<LacreService>()
                .List(GrvId, UsuarioId);

            return result?.Count > 0 ? Ok(result) : NotFound("Lacres sem permissão de acesso ou inexistente");
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
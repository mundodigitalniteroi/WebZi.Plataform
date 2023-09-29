using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebZi.Plataform.Data.Database;
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
        private readonly AppDbContext _context;
        private readonly IServiceProvider _provider;
        private readonly IMapper _mapper;

        public GrvController(AppDbContext context, IServiceProvider provider, IMapper mapper)
        {
            _context = context;
            _provider = provider;
            _mapper = mapper;
        }

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<GrvViewModel>> SelecionarPorId(int GrvId, int UsuarioId)
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

            if (!await _provider.GetService<UsuarioService>().IsUserActive(UsuarioId))
            {
                return BadRequest("Usuário sem permissão de acesso ou inexistente");
            }

            GrvModel result = await _provider
                .GetService<GrvService>()
                .GetById(GrvId, UsuarioId);

            return result != null ? _mapper.Map<GrvViewModel>(result) : NotFound("GRV sem permissão de acesso ou inexistente");
        }

        [HttpGet("SelecionarPorProcesso")]
        public async Task<ActionResult<GrvViewModel>> SelecionarPorProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
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

            if (!await _provider.GetService<UsuarioService>().IsUserActive(UsuarioId))
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

        [HttpGet("ListarStatusOperacao")]
        public async Task<ActionResult<List<StatusOperacaoModel>>> ListarStatusOperacao()
        {
            List<StatusOperacaoModel> result = await _provider
                .GetService<StatusOperacaoService>()
                .List();

            return result?.Count > 0 ? Ok(result) : NotFound("Status Operação não encontrado");
        }

        [HttpGet("ListarLacres")]
        public async Task<ActionResult<List<LacreModel>>> ListarLacres(int GrvId, int UsuarioId)
        {
            if (!await _provider.GetService<UsuarioService>().IsUserActive(UsuarioId))
            {
                return BadRequest("Usuário sem permissão de acesso ou inexistente");
            }

            List<LacreModel> result = await _provider
                .GetService<LacreService>()
                .List(GrvId, UsuarioId);

            return result?.Count > 0 ? Ok(result) : NotFound("Lacres sem permissão de acesso ou inexistente");
        }

        [HttpGet("ListarMotivosApreensoes")]
        public async Task<ActionResult<List<MotivoApreensaoModel>>> ListarMotivosApreensoes()
        {
            List<MotivoApreensaoModel> result = await _context.MotivoApreensao
                .AsNoTracking()
                .ToListAsync();

            return result?.Count > 0 ? Ok(result.OrderBy(o => o.Descricao).ToList()) : NotFound("Motivos de Apreensões não encontrados");
        }
    }
}
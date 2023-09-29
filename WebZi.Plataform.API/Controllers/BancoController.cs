using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Domain.Models.Banco.ViewModel;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly IServiceProvider _provider;
        private readonly IMapper _mapper;

        public BancoController(IServiceProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<BancoViewModel>> SelecionarPorId(short BancoId)
        {
            StringBuilder erros = new();

            if (BancoId <= 0)
            {
                erros.AppendLine("Identificador do Banco inválido");
            }

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return BadRequest(erros.ToString());
            }

            var result = await _provider
                .GetService<BancoService>()
                .GetById(BancoId);

            return result != null ? _mapper.Map<BancoViewModel>(result) : NotFound("Banco não encontrado");
        }

        [HttpGet("ListarBancos")]
        public async Task<ActionResult<List<BancoViewModel>>> ListarBancos()
        {
            var result = await _provider
                .GetService<BancoService>()
                .List();

            return result?.Count > 0 ? _mapper.Map<List<BancoViewModel>>(result) : NotFound("Banco não encontrado");
        }

        [HttpGet("ListarAgenciasBancarias")]
        public async Task<ActionResult<List<AgenciaBancariaViewModel>>> ListarAgenciasBancarias(short BancoId)
        {
            StringBuilder erros = new();

            if (BancoId <= 0)
            {
                erros.AppendLine("Identificador do Banco inválido");
            }

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return BadRequest(erros.ToString());
            }

            var result = await _provider
                .GetService<AgenciaBancariaService>()
                .List(BancoId);

            if (result == null)
            {
                return NotFound("Banco não encontrado");
            }

            return result.AgenciasBancarias.Count > 0 ? _mapper.Map<List<AgenciaBancariaViewModel>>(result.AgenciasBancarias) : NotFound("Agência Bancária não encontrada");
        }
    }
}
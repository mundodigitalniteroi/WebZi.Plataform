using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Documento;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Data.Services.Servico;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Veiculo;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Domain.DTO.Atendimento;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.DTO.Documento;
using WebZi.Plataform.Domain.DTO.Faturamento;
using WebZi.Plataform.Domain.DTO.GRV;
using WebZi.Plataform.Domain.DTO.GRV.Pesquisa;
using WebZi.Plataform.Domain.DTO.Servico;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Veiculo;
using WebZi.Plataform.Domain.DTO.Vistoria;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Data.Services.ClienteDeposito;
using WebZi.Plataform.Data.Services;
using WebZi.Plataform.Data.Services.AutoridadeDivisoes;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReboquistaController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public ReboquistaController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("ListarReboquista")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboquistaListDTO>> ListarReboquista(int IdentificadorCliente, int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboquistaListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .ListReboquistaAsync(IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
        [HttpGet("SelecionarReboquistaPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboquistaListDTO>> SelecionarReboquistaPorIdentificador(int IdentificadorReboquista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboquistaListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboquistaByIdAsync(IdentificadorReboquista);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<MensagemDTO>> Cadastrar(CadastrarReboquistaParameters parameters)
        {
            MensagemDTO ResultView = new();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .CreateReboquistaAsync(parameters);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception e)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(e);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Report;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.DTO.Faturamento.Servico;
using WebZi.Plataform.Domain.DTO.Faturamento.Simulacao;
using WebZi.Plataform.Domain.DTO.Report;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.Faturamento;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturamentoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public FaturamentoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("AlterarFormaPagamento")]
        // TODO: [Authorize]
        public ActionResult<MensagemDTO> AlterarFormaPagamento(int IdentificadorFaturamento, byte IdentificadorNovaFormaPagamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = _provider
                    .GetService<FaturamentoService>()
                    .UpdateFormaPagamento(IdentificadorFaturamento, IdentificadorNovaFormaPagamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarGuiaPagamentoReboqueEstadia")]
        // TODO: [Authorize]
        public async Task<ActionResult<GuiaPagamentoReboqueEstadiaDTO>> GerarGuiaPagamentoReboqueEstadia(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GuiaPagamentoReboqueEstadiaDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GuiaPagamentoReboqueEstadiaService>()
                    .GetGuiaPagamentoReboqueEstadiaAsync(IdentificadorFaturamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarServicoAssociadoTipoVeiculo")]
        // TODO: [Authorize]
        public async Task<ActionResult<ServicoAssociadoTipoVeiculoListDTO>> ListarServicoAssociadoTipoVeiculo(int IdentificadorProcesso, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServicoAssociadoTipoVeiculoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<FaturamentoService>()
                    .ListServicoAssociadoTipoVeiculoAsync(IdentificadorProcesso, IdentificadorUsuario);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarServicoAssociadoGrv")]
        // TODO: [Authorize]
        public async Task<ActionResult<ServicoAssociadoTipoVeiculoListDTO>> ListarServicoAssociadoGrv(int IdentificadorProcesso, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServicoAssociadoTipoVeiculoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<FaturamentoService>()
                    .ListServicoAssociadoTipoVeiculoAsync(IdentificadorProcesso, IdentificadorUsuario);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoCobranca")]
        // TODO: [Authorize]
        public async Task<ActionResult<TabelaGenericaListDTO>> ListarTipoCobranca()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TabelaGenericaListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TabelaGenericaService>()
                    .ListToViewModelAsync("FAT_TIPO_COBRANCA");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("Simulacao")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<SimulacaoDTO>> Simular([FromBody] SimulacaoParameters Parametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SimulacaoDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<FaturamentoService>()
                    .SimularAsync(Parametros);

                ResultView.DataHoraSimulacao = DateTime.Now;

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
    }
}
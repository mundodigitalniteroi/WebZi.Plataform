using Microsoft.AspNetCore.Mvc;
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

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadosMestresController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public DadosMestresController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("ListarAssinaturaCondutor")]
        // TODO: [Authorize]
        public async Task<ActionResult<TabelaGenericaListDTO>> ListarAssinaturaCondutor()
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
                    .ListToViewModelAsync("GRV_ASSINATURA_CONDUTOR");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarAutoridadeResponsavel")]
        // TODO: [Authorize]
        public async Task<ActionResult<AutoridadeResponsavelListDTO>> ListarAutoridadeResponsavel(string UF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AutoridadeResponsavelListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListAutoridadeResponsavelAsync(UF);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarCor")]
        // TODO: [Authorize]
        public async Task<ActionResult<CorListDTO>> ListarCor(string Cor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CorListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<SistemaService>()
                    .ListarCorAsync(Cor);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarEnquadramentoInfracao")]
        // TODO: [Authorize]
        public async Task<ActionResult<EnquadramentoInfracaoListDTO>> ListarEnquadramentoInfracao()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EnquadramentoInfracaoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListEnquadramentoInfracaoAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarEquipamentoOpcional")]
        // TODO: [Authorize]
        public async Task<ActionResult<EquipamentoOpcionalListDTO>> ListarEquipamentoOpcional(byte IdentificadorTipoVeiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EquipamentoOpcionalListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VeiculoService>()
                    .ListEquipamentoOpcionalAsync(IdentificadorTipoVeiculo);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarEstadoGeralVeiculo")]
        // TODO: [Authorize]
        public async Task<ActionResult<TabelaGenericaListDTO>> ListarEstadoGeralVeiculo()
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
                    .ListToViewModelAsync("VISTORIA_ESTADO_GERAL_VEICULO");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarItensParaPesquisa")]
        // TODO: [Authorize]
        public async Task<ActionResult<GrvPesquisaDadosMestresDTO>> ListarItensParaPesquisa(int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GrvPesquisaDadosMestresDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListItemPesquisaAsync(IdentificadorUsuario);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                MensagemDTO error = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)error.HtmlStatusCode, error);
            }
        }

        [HttpGet("ListarMarcaModelo")]
        // TODO: [Authorize]
        public async Task<ActionResult<MarcaModeloListDTO>> ListarMarcaModelo(string MarcaModelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MarcaModeloListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VeiculoService>()
                    .ListMarcaModeloAsync(MarcaModelo);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarMotivoApreensao")]
        // TODO: [Authorize]
        public async Task<ActionResult<MotivoApreensaoListDTO>> ListarMotivoApreensao()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MotivoApreensaoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListMotivoApreensaoAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarOrgaoEmissor")]
        // TODO: [Authorize]
        public async Task<ActionResult<OrgaoEmissorListDTO>> ListarOrgaoEmissor(string UF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrgaoEmissorListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DocumentoService>()
                    .ListOrgaoEmissorAsync(UF);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarProduto")]
        // TODO: [Authorize]
        public async Task<ActionResult<FaturamentoProdutoListDTO>> ListarProduto()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FaturamentoProdutoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<FaturamentoService>()
                    .ListProdutosAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarQualificacaoResponsavel")]
        // TODO: [Authorize]
        public async Task<ActionResult<QualificacaoResponsavelListDTO>> ListarQualificacaoResponsavel()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            QualificacaoResponsavelListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .ListQualificacaoResponsavelAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarReboque")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueListDTO>> ListarReboque(int IdentificadorCliente, int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .ListReboqueAsync(IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
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

        [HttpGet("ListarSituacaoChassi")]
        // TODO: [Authorize]
        public async Task<ActionResult<VistoriaSituacaoChassiListDTO>> ListarSituacaoChassi()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VistoriaSituacaoChassiListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VistoriaService>()
                    .ListSituacaoChassiAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarStatusOperacao")]
        // TODO: [Authorize]
        public async Task<ActionResult<StatusOperacaoListDTO>> ListarStatusOperacao()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StatusOperacaoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListStatusOperacaoAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarStatusVistoria")]
        // TODO: [Authorize]
        public async Task<ActionResult<VistoriaStatusListDTO>> ListarStatusVistoria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VistoriaStatusListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VistoriaService>()
                    .ListStatusVistoriaAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoAvaria")]
        // TODO: [Authorize]
        public async Task<ActionResult<TipoAvariaListDTO>> ListarTipoAvaria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoAvariaListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TipoAvariaService>()
                    .ListTipoAvariaAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoCadastroFotoGGV")]
        // TODO: [Authorize]
        public async Task<ActionResult<TabelaGenericaListDTO>> ListarTipoCadastroFotoGGV()
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
                    .ListToViewModelAsync("GGV_TIPO_CADASTRO_FOTO");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoDirecao")]
        // TODO: [Authorize]
        public async Task<ActionResult<TabelaGenericaListDTO>> ListarTipoDirecao()
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
                    .ListToViewModelAsync("VISTORIA_TIPO_DIRECAO");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoMeioCobranca")]
        // TODO: [Authorize]
        public async Task<ActionResult<TipoMeioCobrancaListDTO>> ListarTipoMeioCobranca()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoMeioCobrancaListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TipoMeioCobrancaService>()
                    .ListAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoVeiculo")]
        // TODO: [Authorize]
        public async Task<ActionResult<TipoVeiculoListDTO>> ListarTipoVeiculo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoVeiculoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VeiculoService>()
                    .ListTipoVeiculoAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueListDTO>> SelecionarReboquePorIdentificador(int IdentificadorReboque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboqueByIdAsync(IdentificadorReboque);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorPlaca")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueListDTO>> SelecionarReboquePorPlaca(string Placa, int IdentificadorCliente, int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboqueByPlacaAsync(Placa, IdentificadorCliente, IdentificadorDeposito);

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
    }
}
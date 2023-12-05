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
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Documento;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.ViewModel.Servico;
using WebZi.Plataform.Domain.ViewModel.Sistema;
using WebZi.Plataform.Domain.ViewModel.Veiculo;
using WebZi.Plataform.Domain.ViewModel.Vistoria;

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
        public async Task<ActionResult<TabelaGenericaViewModelList>> ListarAssinaturaCondutor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TabelaGenericaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TabelaGenericaService>()
                    .ListToViewModelAsync("GRV_ASSINATURA_CONDUTOR");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarAutoridadeResponsavel")]
        // TODO: [Authorize]
        public async Task<ActionResult<AutoridadeResponsavelViewModelList>> ListarAutoridadeResponsavel(string UF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AutoridadeResponsavelViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListAutoridadeResponsavelAsync(UF);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarCor")]
        // TODO: [Authorize]
        public async Task<ActionResult<CorViewModelList>> ListarCor(string Cor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CorViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<SistemaService>()
                    .ListarCorAsync(Cor);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarEnquadramentoInfracao")]
        // TODO: [Authorize]
        public async Task<ActionResult<EnquadramentoInfracaoViewModelList>> ListarEnquadramentoInfracao()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EnquadramentoInfracaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListEnquadramentoInfracaoAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarEquipamentoOpcional")]
        // TODO: [Authorize]
        public async Task<ActionResult<EquipamentoOpcionalViewModelList>> ListarEquipamentoOpcional(byte IdentificadorTipoVeiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EquipamentoOpcionalViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VeiculoService>()
                    .ListarEquipamentoOpcionalAsync(IdentificadorTipoVeiculo);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarEstadoGeralVeiculo")]
        // TODO: [Authorize]
        public async Task<ActionResult<TabelaGenericaViewModelList>> ListarEstadoGeralVeiculo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TabelaGenericaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TabelaGenericaService>()
                    .ListToViewModelAsync("VISTORIA_ESTADO_GERAL_VEICULO");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarItensParaPesquisa")]
        // TODO: [Authorize]
        public async Task<ActionResult<GrvPesquisaDadosMestresViewModel>> ListarItensParaPesquisa(int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GrvPesquisaDadosMestresViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListItemPesquisaAsync(IdentificadorUsuario);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                var error = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)error.HtmlStatusCode, error);
            }
        }

        [HttpGet("ListarMarcaModelo")]
        // TODO: [Authorize]
        public async Task<ActionResult<MarcaModeloViewModelList>> ListarMarcaModelo(string MarcaModelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MarcaModeloViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VeiculoService>()
                    .ListarMarcaModeloAsync(MarcaModelo);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarMotivoApreensao")]
        // TODO: [Authorize]
        public async Task<ActionResult<MotivoApreensaoViewModelList>> ListarMotivoApreensao()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MotivoApreensaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListMotivoApreensaoAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarOrgaoEmissor")]
        // TODO: [Authorize]
        public async Task<ActionResult<OrgaoEmissorViewModelList>> ListarOrgaoEmissor(string UF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrgaoEmissorViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<DocumentoService>()
                    .ListarOrgaoEmissorAsync(UF);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarProduto")]
        // TODO: [Authorize]
        public async Task<ActionResult<FaturamentoProdutoViewModelList>> ListarProduto()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FaturamentoProdutoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<FaturamentoService>()
                    .ListarProdutosAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarQualificacaoResponsavel")]
        // TODO: [Authorize]
        public async Task<ActionResult<QualificacaoResponsavelViewModelList>> ListarQualificacaoResponsavel()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            QualificacaoResponsavelViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AtendimentoService>()
                    .ListQualificacaoResponsavelViewModelAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarReboque")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueViewModelList>> ListarReboque(int IdentificadorCliente, int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .ListReboqueAsync(IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarReboquista")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboquistaViewModelList>> ListarReboquista(int IdentificadorCliente, int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboquistaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .ListReboquistaAsync(IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarSituacaoChassi")]
        // TODO: [Authorize]
        public async Task<ActionResult<VistoriaSituacaoChassiViewModelList>> ListarSituacaoChassi()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VistoriaSituacaoChassiViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VistoriaService>()
                    .ListarSituacaoChassiAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarStatusOperacao")]
        // TODO: [Authorize]
        public async Task<ActionResult<StatusOperacaoViewModelList>> ListarStatusOperacao()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StatusOperacaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListStatusOperacaoAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarStatusVistoria")]
        // TODO: [Authorize]
        public async Task<ActionResult<VistoriaStatusViewModelList>> ListarStatusVistoria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VistoriaStatusViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VistoriaService>()
                    .ListarStatusVistoriaAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoAvaria")]
        // TODO: [Authorize]
        public async Task<ActionResult<TipoAvariaViewModelList>> ListarTipoAvaria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoAvariaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TipoAvariaService>()
                    .ListarTipoAvariaAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoCadastroFotoGGV")]
        // TODO: [Authorize]
        public async Task<ActionResult<TabelaGenericaViewModelList>> ListarTipoCadastroFotoGGV()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TabelaGenericaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TabelaGenericaService>()
                    .ListToViewModelAsync("GGV_TIPO_CADASTRO_FOTO");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoDirecao")]
        // TODO: [Authorize]
        public async Task<ActionResult<TabelaGenericaViewModelList>> ListarTipoDirecao()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TabelaGenericaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TabelaGenericaService>()
                    .ListToViewModelAsync("VISTORIA_TIPO_DIRECAO");

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoMeioCobranca")]
        // TODO: [Authorize]
        public ActionResult<TipoMeioCobrancaViewModelList> ListarTipoMeioCobranca()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoMeioCobrancaViewModelList ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<TipoMeioCobrancaService>()
                    .List();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoVeiculo")]
        // TODO: [Authorize]
        public async Task<ActionResult<TipoVeiculoViewModelList>> ListarTipoVeiculo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoVeiculoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VeiculoService>()
                    .ListarTipoVeiculoAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueViewModelList>> SelecionarReboquePorIdentificador(int IdentificadorReboque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboqueByIdAsync(IdentificadorReboque);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorPlaca")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboqueViewModelList>> SelecionarReboquePorPlaca(string Placa, int IdentificadorCliente, int IdentificadorDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboqueByPlacaAsync(Placa, IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquistaPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<ReboquistaViewModelList>> SelecionarReboquistaPorIdentificador(int IdentificadorReboquista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReboquistaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetByReboquistaIdAsync(IdentificadorReboquista);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
    }
}
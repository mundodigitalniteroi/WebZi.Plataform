using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Veiculo;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
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
        public async Task<ActionResult<VistoriaEstadoGeralVeiculoViewModelList>> ListarEstadoGeralVeiculo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VistoriaEstadoGeralVeiculoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VistoriaService>()
                    .ListarEstadoGeralVeiculoAsync();

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
                    .ListarItemPesquisaAsync(IdentificadorUsuario);

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
                    .ListarMotivoApreensaoAsync();

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
                    .GetService<GrvService>()
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
                    .GetService<QualificacaoResponsavelService>()
                    .ListAsync();

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

        [HttpGet("ListarStatusAssinaturaCondutor")]
        // TODO: [Authorize]
        public async Task<ActionResult<StatusOperacaoViewModelList>> ListarStatusAssinaturaCondutor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StatusAssinaturaCondutorViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListarStatusAssinaturaCondutorAsync();

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
                    .ListarStatusOperacaoAsync();

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

        [HttpGet("ListarTipoDirecao")]
        // TODO: [Authorize]
        public async Task<ActionResult<VistoriaTipoDirecaoViewModelList>> ListarTipoDirecao()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VistoriaTipoDirecaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VistoriaService>()
                    .ListarTipoDirecaoAsync();

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
    }
}
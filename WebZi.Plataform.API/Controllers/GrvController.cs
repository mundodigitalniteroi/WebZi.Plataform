using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Servico;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Veiculo;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.ViewModel.Servico;
using WebZi.Plataform.Domain.ViewModel.Sistema;
using WebZi.Plataform.Domain.ViewModel.Veiculo;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrvController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public GrvController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("Cadastrar")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<GrvCadastradoViewModel>> Cadastrar(GrvCadastroViewModel Grv)
        {
            GrvCadastradoViewModel ResultView = new();

            try
            {
                ResultView.Mensagem = await _provider
                    .GetService<GrvService>()
                    .ValidarInformacoesParaCadastro(Grv);

                if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView.Mensagem);
                }
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }

            try
            {
                ResultView = _provider
                    .GetService<GrvService>()
                    .Create(Grv);

                if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView.Mensagem);
                }
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }

            return ResultView;
        }

        [HttpPost("CadastrarFotos")]
        [IgnoreAntiforgeryToken]
        public ActionResult<MensagemViewModel> CadastrarFotos(GrvFotoViewModel Fotos)
        {
            MensagemViewModel ResultView;

            try
            {
                ResultView = _provider
                    .GetService<GrvService>()
                    .SendFiles(Fotos);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarAutoridadeResponsavel")]
        public async Task<ActionResult<AutoridadeResponsavelViewModelList>> ListarAutoridadeResponsavel(string UF)
        {
            AutoridadeResponsavelViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListAutoridadeResponsavel(UF);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarCor")]
        public async Task<ActionResult<CorViewModelList>> ListarCor(string Cor)
        {
            CorViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<SistemaService>()
                    .ListarCores(Cor);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarFoto")]
        public async Task<ActionResult<ImageViewModelList>> ListarFoto(int IdentificadorGrv, int IdentificadorUsuario)
        {
            ImageViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListarFotos(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarItensParaPesquisa")]
        public async Task<ActionResult<GrvPesquisaDadosMestresViewModel>> ListarItensParaPesquisa(int IdentificadorUsuario)
        {
            GrvPesquisaDadosMestresViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListarItensPesquisa(IdentificadorUsuario);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                var error = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)error.HtmlStatusCode, error);
            }
        }

        [HttpGet("ListarLacre")]
        public async Task<ActionResult<LacreViewModelList>> ListarLacre(int IdentificadorGrv, int IdentificadorUsuario)
        {
            LacreViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListarLacre(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarMarcaModelo")]
        public async Task<ActionResult<MarcaModeloViewModelList>> ListarMarcaModelo(string MarcaModelo)
        {
            MarcaModeloViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VeiculoService>()
                    .ListarMarcaModelo(MarcaModelo);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarMotivoApreensao")]
        public async Task<ActionResult<MotivoApreensaoViewModelList>> ListarMotivoApreensao()
        {
            MotivoApreensaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListarMotivoApreensao();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarProduto")]
        public async Task<ActionResult<FaturamentoProdutoViewModelList>> ListarProduto()
        {
            FaturamentoProdutoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListarProdutos();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarReboque")]
        public async Task<ActionResult<ReboqueViewModelList>> ListarReboque(int IdentificadorCliente, int IdentificadorDeposito)
        {
            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .ListarReboque(IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarReboquista")]
        public async Task<ActionResult<ReboquistaViewModelList>> ListarReboquista(int IdentificadorCliente, int IdentificadorDeposito)
        {
            ReboquistaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .ListarReboquista(IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarStatusAssinaturaCondutor")]
        public async Task<ActionResult<StatusOperacaoViewModelList>> ListarStatusAssinaturaCondutor()
        {
            StatusAssinaturaCondutorViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListarStatusAssinaturaCondutor();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarStatusOperacao")]
        public async Task<ActionResult<StatusOperacaoViewModelList>> ListarStatusOperacao()
        {
            StatusOperacaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListarStatusOperacao();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTipoVeiculo")]
        public async Task<ActionResult<TipoVeiculoViewModelList>> ListarTipoVeiculo()
        {
            TipoVeiculoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<VeiculoService>()
                    .ListarTipoVeiculo();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("Pesquisar")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<GrvViewModelList>> Pesquisar(GrvPesquisaInputViewModel ParametrosPesquisa)
        {
            GrvPesquisaResultViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .Search(ParametrosPesquisa);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorIdentificador")]
        public async Task<ActionResult<GrvViewModel>> SelecionarPorIdentificador(int IdentificadorGrv, int IdentificadorUsuario)
        {
            GrvViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetById(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorProcesso")]
        public async Task<ActionResult<GrvViewModelList>> SelecionarPorProcesso(string NumeroProcesso, string CodigoProduto, int IdentificadorCliente, int IdentificadorDeposito, int IdentificadorUsuario)
        {
            GrvViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetByNumeroFormularioGrv(NumeroProcesso, CodigoProduto, IdentificadorCliente, IdentificadorDeposito, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorIdentificador")]
        public async Task<ActionResult<ReboqueViewModelList>> SelecionarReboquePorIdentificador(int IdentificadorReboque)
        {
            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboqueById(IdentificadorReboque);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorPlaca")]
        public async Task<ActionResult<ReboqueViewModelList>> SelecionarReboquePorPlaca(string Placa, int IdentificadorCliente, int IdentificadorDeposito)
        {
            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetReboqueByPlaca(Placa, IdentificadorCliente, IdentificadorDeposito);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquistaPorIdentificador")]
        public async Task<ActionResult<ReboquistaViewModelList>> SelecionarReboquistaPorIdentificador(int IdentificadorReboquista)
        {
            ReboquistaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ServicoService>()
                    .GetByReboquistaId(IdentificadorReboquista);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("ValidarInformacoesParaCadastro")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemViewModel>> ValidarInformacoesParaCadastro(GrvCadastroViewModel Grv)
        {
            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ValidarInformacoesParaCadastro(Grv);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("VerificarAlteracaoStatusProcesso")]
        public async Task<ActionResult<MensagemViewModel>> VerificarAlteracaoStatusProcesso(int IdentificadorGrv, string IdentificadorStatusOperacao, int IdentificadorUsuario)
        {
            MensagemViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .VerificarAlteracaoStatusGRV(IdentificadorGrv, IdentificadorStatusOperacao, IdentificadorUsuario);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }
    }
}
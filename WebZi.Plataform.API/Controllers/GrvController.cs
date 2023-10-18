using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Data.Services.Servico;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Veiculo;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
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

        [HttpGet("ListarAutoridadesResponsaveis")]
        public async Task<ActionResult<AutoridadeResponsavelViewModelList>> ListarAutoridadesResponsaveis(string UF)
        {
            AutoridadeResponsavelViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AutoridadeResponsavelService>()
                    .List(UF);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarCores")]
        public async Task<ActionResult<CorViewModelList>> ListarCores(string Cor)
        {
            CorViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<CorService>()
                    .List(Cor);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarLacres")]
        public async Task<ActionResult<LacreViewModelList>> ListarLacres(int GrvId, int UsuarioId)
        {
            LacreViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<LacreService>()
                    .List(GrvId, UsuarioId);

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
                    .GetService<MarcaModeloService>()
                    .List(MarcaModelo);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarMotivosApreensoes")]
        public async Task<ActionResult<MotivoApreensaoViewModelList>> ListarMotivosApreensoes()
        {
            MotivoApreensaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<MotivoApreensaoService>()
                    .List();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarReboques")]
        public async Task<ActionResult<ReboqueViewModelList>> ListarReboques(int ClienteId, int DepositoId)
        {
            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ReboqueService>()
                    .List(ClienteId, DepositoId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarReboquistas")]
        public async Task<ActionResult<ReboquistaViewModelList>> ListarReboquistas(int ClienteId, int DepositoId)
        {
            ReboquistaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ReboquistaService>()
                    .List(ClienteId, DepositoId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarStatusOperacoes")]
        public async Task<ActionResult<StatusOperacaoViewModelList>> ListarStatusOperacoes()
        {
            StatusOperacaoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<StatusOperacaoService>()
                    .List();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarTiposVeiculos")]
        public async Task<ActionResult<TipoVeiculoViewModelList>> ListarTiposVeiculos()
        {
            TipoVeiculoViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<TipoVeiculoService>()
                    .List();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("Pesquisar")]
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

        [HttpGet("SelecionarPorId")]
        public async Task<ActionResult<GrvViewModel>> SelecionarPorId(int GrvId, int UsuarioId)
        {
            GrvViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetById(GrvId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorProcesso")]
        public async Task<ActionResult<GrvViewModelList>> SelecionarPorProcesso(string NumeroProcesso, int ClienteId, int DepositoId, int UsuarioId)
        {
            GrvViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetByNumeroFormularioGrv(NumeroProcesso, ClienteId, DepositoId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorId")]
        public async Task<ActionResult<ReboqueViewModelList>> SelecionarReboquePorId(int ReboqueId)
        {
            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ReboqueService>()
                    .GetById(ReboqueId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquePorPlaca")]
        public async Task<ActionResult<ReboqueViewModelList>> SelecionarReboquePorPlaca(string Placa, int ClienteId, int DepositoId)
        {
            ReboqueViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ReboqueService>()
                    .GetByPlaca(Placa, ClienteId, DepositoId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarReboquistaPorId")]
        public async Task<ActionResult<ReboquistaViewModelList>> SelecionarReboquistaPorId(int ReboquistaId)
        {
            ReboquistaViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<ReboquistaService>()
                    .GetById(ReboquistaId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("ValidarInformacoesParaCadastro")]
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
    }
}
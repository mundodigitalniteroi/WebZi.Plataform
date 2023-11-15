using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Bucket;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;

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
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ResultadoCadastroGrvViewModel>> Cadastrar([FromBody] CadastroGrvViewModel Grv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResultadoCadastroGrvViewModel ResultView = new();

            try
            {
                ResultView.Mensagem = await _provider
                    .GetService<GrvService>()
                    .ValidarInformacoesPersistenciaAsync(Grv);

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
                    .InsertGrv(Grv);

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

        [HttpPost("CadastrarAssinaturaAgente")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemViewModel>> CadastrarAssinaturaAgente([FromBody] BucketCadastroViewModel Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .InsertAssinaturaAgente(Json.IdentificadorTabelaOrigem, Json.IdentificadorUsuario, Json.Imagem);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("CadastrarAssinaturaCondutor")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemViewModel>> CadastrarAssinaturaCondutor([FromBody] BucketCadastroViewModel Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .InsertAssinaturaCondutor(Json.IdentificadorTabelaOrigem, Json.IdentificadorUsuario, Json.Imagem);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("CadastrarDocumentoCondutor")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public ActionResult<MensagemViewModel> CadastrarDocumentoCondutor([FromBody] CadastroCondutorDocumentoViewModelList ListagemDocumentoCondutor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = _provider
                    .GetService<GrvService>()
                    .InsertDocumentosCondutor(ListagemDocumentoCondutor);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("CadastrarFotos")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public ActionResult<MensagemViewModel> CadastrarFotos([FromBody] CadastroFotoGrvViewModel Fotos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = _provider
                    .GetService<GrvService>()
                    .InsertFotos(Fotos);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("CadastrarLacres")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemViewModel>> CadastrarLacres(int IdentificadorGrv, int IdentificadorUsuario, [FromBody] List<string> ListagemLacre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .InsertLacresAsync(IdentificadorGrv, IdentificadorUsuario, ListagemLacre);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirAssinaturaAgente")]
        // TODO: [Authorize]
        public async Task<ActionResult<MensagemViewModel>> ExcluirAssinaturaAgente(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteAssinaturaAgente(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirAssinaturaCondutor")]
        // TODO: [Authorize]
        public async Task<ActionResult<MensagemViewModel>> ExcluirAssinaturaCondutor(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteAssinaturaCondutor(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirFotos")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemViewModel>> ExcluirFotos(int IdentificadorGrv, int IdentificadorUsuario, [FromBody] List<int> ListagemIdentificadorTabelaOrigem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteFotosAsync(IdentificadorGrv, IdentificadorUsuario, ListagemIdentificadorTabelaOrigem);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirGrv")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemViewModel>> ExcluirGrv(int IdentificadorGrv, string Login, [DataType(DataType.Password)] string Senha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteGrvAsync(IdentificadorGrv, Login, Senha);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirGrvPorProcesso")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemViewModel>> ExcluirGrvPorProcesso(string NumeroProcesso, string CodigoProduto, int IdentificadorCliente, int IdentificadorDeposito, string Login, [DataType(DataType.Password)] string Senha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteGrvAsync(NumeroProcesso, CodigoProduto, IdentificadorCliente, IdentificadorDeposito, Login, Senha);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirLacres")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemViewModel>> ExcluirLacres(int IdentificadorGrv, int IdentificadorUsuario, [FromBody] List<int> ListagemIdentificadorLacre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteLacresAsync(IdentificadorGrv, IdentificadorUsuario, ListagemIdentificadorLacre);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarDocumentosCondutor")]
        // TODO: [Authorize]
        public async Task<ActionResult<ImageViewModelList>> ListarDocumentosCondutor(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListDocumentosCondutorAsync(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarFotos")]
        // TODO: [Authorize]
        public async Task<ActionResult<ImageViewModelList>> ListarFotos(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListFotoAsync(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarLacres")]
        // TODO: [Authorize]
        public async Task<ActionResult<LacreViewModelList>> ListarLacres(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LacreViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListLacreAsync(IdentificadorGrv, IdentificadorUsuario);

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
        // TODO: [Authorize]
        public async Task<ActionResult<GrvViewModelList>> Pesquisar([FromBody] GrvPesquisaInputViewModel ParametrosPesquisa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GrvPesquisaResultViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .SearchAsync(ParametrosPesquisa);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarAssinaturaAgente")]
        // TODO: [Authorize]
        public async Task<ActionResult<GrvViewModelList>> SelecionarAssinaturaAgente(int GrvId, int UsuarioId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetAssinaturaAgenteAsync(GrvId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarAssinaturaCondutor")]
        // TODO: [Authorize]
        public async Task<ActionResult<GrvViewModelList>> SelecionarAssinaturaCondutor(int GrvId, int UsuarioId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetAssinaturaCondutorAsync(GrvId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<GrvViewModel>> SelecionarPorIdentificador(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GrvViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetByIdAsync(IdentificadorGrv, IdentificadorUsuario);

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
        // TODO: [Authorize]
        public async Task<ActionResult<MensagemViewModel>> ValidarInformacoesParaCadastro([FromBody] CadastroGrvViewModel Grv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ValidarInformacoesPersistenciaAsync(Grv);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("VerificarAlteracaoStatusProcesso")]
        // TODO: [Authorize]
        public async Task<ActionResult<MensagemViewModel>> VerificarAlteracaoStatusProcesso(int IdentificadorGrv, string IdentificadorStatusOperacao, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .VerificarAlteracaoStatusGRVAsync(IdentificadorGrv, IdentificadorStatusOperacao, IdentificadorUsuario);

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
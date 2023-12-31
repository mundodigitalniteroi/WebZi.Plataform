using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Bucket;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.GRV;
using WebZi.Plataform.Domain.DTO.GRV.Cadastro;
using WebZi.Plataform.Domain.DTO.GRV.Pesquisa;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Services.GRV;
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
        public async Task<ActionResult<ResultadoCadastroGrvDTO>> Cadastrar([FromBody] GrvParameters Grv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResultadoCadastroGrvDTO ResultView = new();

            try
            {
                ResultView.Mensagem = await _provider
                    .GetService<GrvService>()
                    .CheckInformacoesPersistenciaAsync(Grv);

                if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView.Mensagem);
                }
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }

            try
            {
                ResultView = _provider
                    .GetService<GrvService>()
                    .CreateGrv(Grv);

                if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView.Mensagem);
                }
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }

            return ResultView;
        }

        [HttpPost("CadastrarAssinaturaAgente")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> CadastrarAssinaturaAgente([FromBody] BucketDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .CreateAssinaturaAgenteAsync(Json.IdentificadorTabelaOrigem, Json.IdentificadorUsuario, Json.Imagem);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("CadastrarAssinaturaCondutor")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> CadastrarAssinaturaCondutor([FromBody] BucketDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .CreateAssinaturaCondutorAsync(Json.IdentificadorTabelaOrigem, Json.IdentificadorUsuario, Json.Imagem);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("CadastrarDocumentoCondutor")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public ActionResult<MensagemDTO> CadastrarDocumentoCondutor([FromBody] CondutorDocumentoParametersList ListagemDocumentoCondutor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = _provider
                    .GetService<GrvService>()
                    .CreateDocumentosCondutor(ListagemDocumentoCondutor);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("CadastrarFotos")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public ActionResult<MensagemDTO> CadastrarFotos([FromBody] FotoGrvParameters Fotos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = _provider
                    .GetService<GrvService>()
                    .CreateFotos(Fotos);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("CadastrarLacres")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> CadastrarLacres(int IdentificadorProcesso, int IdentificadorUsuario, [FromBody] List<string> ListagemLacre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .CreateLacresAsync(IdentificadorProcesso, IdentificadorUsuario, ListagemLacre);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirAssinaturaAgente")]
        // TODO: [Authorize]
        public async Task<ActionResult<MensagemDTO>> ExcluirAssinaturaAgente(int IdentificadorProcesso, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteAssinaturaAgenteAsync(IdentificadorProcesso, IdentificadorUsuario);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirAssinaturaCondutor")]
        // TODO: [Authorize]
        public async Task<ActionResult<MensagemDTO>> ExcluirAssinaturaCondutor(int IdentificadorProcesso, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteAssinaturaCondutorAsync(IdentificadorProcesso, IdentificadorUsuario);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirFotos")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> ExcluirFotos(int IdentificadorProcesso, int IdentificadorUsuario, [FromBody] List<int> ListagemIdentificadorTabelaOrigem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteFotosAsync(IdentificadorProcesso, IdentificadorUsuario, ListagemIdentificadorTabelaOrigem);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirGrv")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> ExcluirGrv(int IdentificadorProcesso, string Login, [DataType(DataType.Password)] string Senha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteGrvAsync(IdentificadorProcesso, Login, Senha);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirGrvPorProcesso")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> ExcluirGrvPorProcesso(string NumeroProcesso, string CodigoProduto, int IdentificadorCliente, int IdentificadorDeposito, string Login, [DataType(DataType.Password)] string Senha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteGrvAsync(NumeroProcesso, CodigoProduto, IdentificadorCliente, IdentificadorDeposito, Login, Senha);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirLacres")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> ExcluirLacres(int IdentificadorProcesso, int IdentificadorUsuario, [FromBody] List<int> ListagemIdentificadorLacre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .DeleteLacresAsync(IdentificadorProcesso, IdentificadorUsuario, ListagemIdentificadorLacre);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarDocumentosCondutor")]
        // TODO: [Authorize]
        public async Task<ActionResult<ImageListDTO>> ListarDocumentosCondutor(int IdentificadorProcesso, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListDocumentosCondutorAsync(IdentificadorProcesso, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarFotos")]
        // TODO: [Authorize]
        public async Task<ActionResult<ImageListDTO>> ListarFotos(int IdentificadorProcesso, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .ListFotoAsync(IdentificadorProcesso, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarLacres")]
        // TODO: [Authorize]
        public async Task<ActionResult<LacreViewModelList>> ListarLacres(int IdentificadorProcesso, int IdentificadorUsuario)
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
                    .ListLacreAsync(IdentificadorProcesso, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("Pesquisar")]
        [IgnoreAntiforgeryToken]
        // TODO: [Authorize]
        public async Task<ActionResult<GrvViewModelList>> Pesquisar([FromBody] GrvPesquisaParameters ParametrosPesquisa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GrvPesquisaResultListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .SearchAsync(ParametrosPesquisa);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

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

            ImageListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetAssinaturaAgenteAsync(GrvId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

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

            ImageListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .GetAssinaturaCondutorAsync(GrvId, UsuarioId);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<GrvDTO>> SelecionarPorIdentificador(int IdentificadorProcesso, int IdentificadorUsuario)
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
                    .GetByIdAsync(IdentificadorProcesso, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("ValidarInformacoesParaCadastro")]
        [IgnoreAntiforgeryToken]
        // TODO: [Authorize]
        public async Task<ActionResult<MensagemDTO>> ValidarInformacoesParaCadastro([FromBody] GrvParameters Grv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .CheckInformacoesPersistenciaAsync(Grv);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("VerificarAlteracaoStatusProcesso")]
        // TODO: [Authorize]
        public async Task<ActionResult<MensagemDTO>> VerificarAlteracaoStatusProcesso(int IdentificadorProcesso, string IdentificadorStatusOperacao, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GrvService>()
                    .CheckAlteracaoStatusGrvAsync(IdentificadorProcesso, IdentificadorStatusOperacao, IdentificadorUsuario);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }
    }
}
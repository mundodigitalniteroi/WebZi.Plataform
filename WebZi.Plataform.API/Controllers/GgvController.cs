using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.GGV;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.GGV;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GgvController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public GgvController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("Cadastrar")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> Cadastrar([FromBody] GgvParameters Ggv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .ValidarInformacoesPersistenciaAsync(Ggv);

                if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
                }
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .CreateGgvAsync(Ggv);

                if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
                }
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }

            return ResultView;
        }

        [HttpPost("CadastrarFotos")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemDTO>> CadastrarFotos([FromBody] FotoGgvParameters Fotos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemDTO ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .CreateFotosAsync(Fotos);

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
                    .GetService<GgvService>()
                    .DeleteFotosAsync(IdentificadorProcesso, IdentificadorUsuario, ListagemIdentificadorTabelaOrigem);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarDadosMestres")]
        // TODO: [Authorize]
        public async Task<ActionResult<DadosMestresDTO>> ListarDadosMestres(int GrvId, int UsuarioId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DadosMestresDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .ListDadosMestresAsync(GrvId, UsuarioId);

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
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
                    .GetService<GgvService>()
                    .ListFotosAsync(IdentificadorProcesso, IdentificadorUsuario);

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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Domain.DTO.GRV;
using WebZi.Plataform.Domain.DTO.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;

namespace WebZi.Plataform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VLockController : ControllerBase
{
    private readonly IServiceProvider _provider;

    public VLockController(IServiceProvider provider)
    {
        _provider = provider;
    }


    [HttpPost("Cadastrar")]
    public async Task<ActionResult<ResultadoCadastroGrvDTO>> Cadastrar([FromBody] GrvVLockParameters Grv,
        CancellationToken ct)
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
                .CheckInformacoesPersistenciaAsync(Grv, ct);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView.Mensagem);
            }

            if (ResultView.Mensagem.AvisosInformativos.Count > 0)
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
            ResultView = await _provider
                .GetService<GrvService>()
                .CreateVlockGrv(Grv, ct);

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

    [HttpPost("2ViaImpressaoVlock")]
    [IgnoreAntiforgeryToken]
    // TODO: [Authorize]
    public async Task<ActionResult<Get2ViaVlockDTO>> Get2ViaImpressaoVlock(
        [FromBody] Get2ViaVlockParameters parameters,
        CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Get2ViaVlockDTO ResultView = new();
        var user = User.GetUserId() ?? parameters?.IdentificadorUsuario ?? 0;
        var numProc = parameters?.NumeroProcesso ?? "";

        try
        {
            ResultView = await _provider
                .GetService<GrvService>()
                .Get2ViaVlockAsync(numProc, user, ct);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }
}
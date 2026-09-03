using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Domain.DTO.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;

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
    public async Task<ActionResult<ResultadoCadastroGrvDTO>> Cadastrar([FromBody] GrvVLockParameters Grv, CancellationToken ct)
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

}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Pessoa;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.Pessoa;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.WebServices.Nfse;

namespace WebZi.Plataform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NfeController : ControllerBase
{
    private readonly IServiceProvider _provider;

    public NfeController(IServiceProvider provider)
    {
        _provider = provider;
    }

    [HttpPost("ReprocessarNota")]
    public async Task<ActionResult<MensagemDTO>> ReprocessarNota(int grvId, int notaId, int usuarioId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        MensagemDTO ResultView = new();

        try
        {
            ResultView = await _provider
                .GetService<WSNfseService>()
                .ReprocessNfseAsync(grvId, notaId, usuarioId);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    [HttpPost("GerarNota")]
    public async Task<ActionResult<MensagemDTO>> GerarNota(int grvId, int usuarioId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        MensagemDTO ResultView = new();

        try
        {
            ResultView = await _provider
                .GetService<WSNfseService>()
                .CreateNfseAsync(grvId, usuarioId);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }
}
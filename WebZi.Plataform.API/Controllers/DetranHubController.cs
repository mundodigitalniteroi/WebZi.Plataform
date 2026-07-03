using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.DetranHub;
using WebZi.Plataform.Domain.DTO.DetranHub;

namespace WebZi.Plataform.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DetranHubController : ControllerBase
{
    private readonly IServiceProvider _provider;

    public DetranHubController(IServiceProvider provider)
    {
        _provider = provider;
    }

    [HttpGet("ConsultarPorPlacaOuChassi")]
    public async Task<ActionResult<ConsultarPorPlacaOuChassiDTO>> ConsultarPorPlacaOuChassi(string? placa, string? chassi)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        ConsultarPorPlacaOuChassiDTO ResultView = new();

        try
        {
            ResultView = await _provider
                .GetService<DetranHubService>()
                .SearchToPlateOrChassi(placa, chassi);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }
}
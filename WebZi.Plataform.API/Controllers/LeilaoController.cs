using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Domain.DTO.Leilao;
using WebZi.Plataform.Domain.ViewModel.Liberacao;

namespace WebZi.Plataform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LeilaoController : ControllerBase
{
    private readonly IServiceProvider _provider;

    public LeilaoController(IServiceProvider provider)
    {
        _provider = provider;
    }

    [HttpPost("ListarProcessosPreLeilao")]
    public async Task<ActionResult<PreLeilaoListDTO>> ListarProcessosPreLeilao(ProcessosPreLeilaoParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        PreLeilaoListDTO ResultView = new();

        try
        {
            ResultView = await _provider.GetService<LeilaoService>().ListPreLeiloesAsync(parameters);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }
}
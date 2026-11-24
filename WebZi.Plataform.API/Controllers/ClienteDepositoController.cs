using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.ClienteDeposito;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Domain.DTO.Cliente;
using WebZi.Plataform.Domain.DTO.ClienteDeposito;

namespace WebZi.Plataform.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class ClienteDepositoController : ControllerBase
{
    private readonly IServiceProvider _provider;

    public ClienteDepositoController(IServiceProvider provider)
    {
        _provider = provider;
    }


    [HttpGet("{identificador}/SelecionarPorIdentificador")]
    public async Task<ActionResult<GetPorIdentificadorDTO>> SelecionarPorIdentificador([FromRoute] int identificador, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        GetPorIdentificadorDTO ResultView = new();

        var userId = User.GetUserId();

        try
        {
            ResultView = await _provider
                .GetService<ClienteDepositoService>()
                .GetPorIdentificadorAsync(userId.Value, identificador,  ct);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }
}

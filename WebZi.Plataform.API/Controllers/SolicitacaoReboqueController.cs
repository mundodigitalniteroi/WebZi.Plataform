using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SolicitacaoReboqueController : ControllerBase
{
    private readonly IServiceProvider _provider;

    public SolicitacaoReboqueController(IServiceProvider provider)
    {
        _provider = provider;
    }

    [HttpGet]
    public async Task<ActionResult<SolicitacoesReboqueListDTO>> ListaSolicitacoesParaReboque(CancellationToken ct)
    {
        SolicitacoesReboqueListDTO ResultView = new();

        var userId = User.GetUserId();
        try
        {
            // ResultView = await _provider
            //     .GetService<ServicoService>()
            //     .ListReboquistaAsync(IdentificadorCliente, IdentificadorDeposito);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }
}
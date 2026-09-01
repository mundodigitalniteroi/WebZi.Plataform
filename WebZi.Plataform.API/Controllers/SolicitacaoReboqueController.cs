using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.SolicitacaoReboque;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.GRV.SolicitacaoReboque;

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
    public async Task<ActionResult<SolicitacoesReboqueListDTO>> ListaSolicitacoesParaReboque(short skip, short take,
        CancellationToken ct)
    {
        SolicitacoesReboqueListDTO ResultView = new();

        var userId = User.GetUserId();
        try
        {
            ResultView = await _provider
                .GetService<SolicitacaoReboqueService>()
                .ListSolicitacoesReboqueAsync(userId!.Value, skip, take, ct);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }

    [HttpGet]
    public async Task<ActionResult<SolicitacaoReboqueDTO>> SelecionarSolicitacaoReboque(int solicitacaoReboqueId,
        CancellationToken ct)
    {
        SolicitacaoReboqueDTO ResultView = new();

        var userId = User.GetUserId();
        try
        {
            ResultView = await _provider
                .GetService<SolicitacaoReboqueService>()
                .GetByIdSolicitacaoReboqueAsync(userId!.Value, solicitacaoReboqueId, ct);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }

    [HttpPost]
    public async Task<ActionResult<MensagemDTO>> CadastrarSolicitacaoReboque(
        [FromBody] CadastrarSolicitacaoReboqueParameters cadastrarSolicitacaoReboque, CancellationToken ct)
    {
        MensagemDTO ResultView = new();

        var userId = User.GetUserId();
        cadastrarSolicitacaoReboque.IdentificadorUsuario = userId!.Value;

        try
        {
            ResultView = await _provider
                .GetService<SolicitacaoReboqueService>()
                .CreateSolicitacaoReboqueAsync(cadastrarSolicitacaoReboque, ct);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    [HttpPut]
    public async Task<ActionResult<MensagemDTO>> CancelarSolicitacaoReboque(
        int solicitacaoReboqueId,
        CancellationToken ct)
    {
        MensagemDTO ResultView = new();

        var userId = User.GetUserId();

        try
        {
            ResultView = await _provider
                .GetService<SolicitacaoReboqueService>()
                .CancelarSolicitacaoReboqueAsync(userId!.Value, solicitacaoReboqueId, ct);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    [HttpPut]
    public async Task<ActionResult<MensagemDTO>> AtualizarSolicitacaoReboque(
        [FromBody] AtualizarSolicitacaoReboqueParameters solicitacaoReboque, CancellationToken ct)
    {
        MensagemDTO ResultView = new();

        var userId = User.GetUserId();
        solicitacaoReboque.IdentificadorUsuario = userId!.Value;

        try
        {
            ResultView = await _provider
                .GetService<SolicitacaoReboqueService>()
                .UpdateSolicitacaoReboqueAsync(solicitacaoReboque, ct);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    [HttpPut("aceitar")]
    public async Task<ActionResult<MensagemDTO>> AceitarSolicitacaoReboque(
        [FromBody] AceitarSolicitacaoReboqueParameters aceitarSolicitacaoReboque, CancellationToken ct)
    {
        MensagemDTO ResultView = new();

        var userId = User.GetUserId();
        aceitarSolicitacaoReboque.IdentificadorUsuario = userId!.Value;

        try
        {
            ResultView = await _provider
                .GetService<SolicitacaoReboqueService>()
                .AceitarSolicitacaoReboqueAsync(aceitarSolicitacaoReboque, ct);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }
}
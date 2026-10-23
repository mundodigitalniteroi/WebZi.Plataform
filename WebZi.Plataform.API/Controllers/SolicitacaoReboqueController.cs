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

    [HttpPost("ListarSolicitacoes")]
    public async Task<ActionResult<SolicitacoesReboqueListDTO>> ListarSolicitacoes(
        [FromBody] SolicitacaoReboquePesquisaParameters? parameters,
        CancellationToken ct)
    {
        SolicitacoesReboqueListDTO ResultView = new();

        var userId = User.GetUserId();
        parameters.IdentificadorUsuario = userId.Value;
        try
        {
            ResultView = await _provider
                .GetService<SolicitacaoReboqueService>()
                .ListSolicitacoesReboqueAsync(
                    parameters,
                    ct
                );

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }

    [HttpGet("{solicitacaoReboqueId}/SelecionarSolicitacao")]
    public async Task<ActionResult<SelecionarSolicitacaoReboqueDTO>> SelecionarSolicitacaoReboque(
        [FromRoute] int solicitacaoReboqueId,
        CancellationToken ct)
    {
        SelecionarSolicitacaoReboqueDTO ResultView = new();

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

    [HttpPost("Cadastrar")]
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

    [HttpPut("{solicitacaoReboqueId}/Cancelar")]
    public async Task<ActionResult<MensagemDTO>> CancelarSolicitacaoReboque(
        [FromRoute] int solicitacaoReboqueId,
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

    [HttpPut("{solicitacaoReboqueId}/Atualizar")]
    public async Task<ActionResult<MensagemDTO>> AtualizarSolicitacaoReboque(
        [FromRoute] int solicitacaoReboqueId,
        [FromBody] AtualizarSolicitacaoReboqueParameters parameters, CancellationToken ct)
    {
        MensagemDTO ResultView = new();

        var userId = User.GetUserId();
        parameters.IdentificadorUsuario = userId!.Value;
        parameters.SolicitacaoReboqueId = solicitacaoReboqueId;

        try
        {
            ResultView = await _provider
                .GetService<SolicitacaoReboqueService>()
                .UpdateSolicitacaoReboqueAsync(parameters, ct);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    [HttpPut("{solicitacaoReboqueId}/aceitar")]
    public async Task<ActionResult<MensagemDTO>> AceitarSolicitacaoReboque(
        [FromRoute] int solicitacaoReboqueId,
        [FromBody] AceitarSolicitacaoReboqueParameters parameters,
        CancellationToken ct)
    {
        MensagemDTO ResultView = new();

        var userId = User.GetUserId();
        parameters.IdentificadorUsuario = userId!.Value;
        parameters.SolicitacaoReboqueId = solicitacaoReboqueId;
        try
        {
            ResultView = await _provider
                .GetService<SolicitacaoReboqueService>()
                .AceitarSolicitacaoReboqueAsync(parameters, ct);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }
}
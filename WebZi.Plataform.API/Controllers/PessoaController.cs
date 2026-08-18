using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Pessoa;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Domain.DTO.Pessoa;
using WebZi.Plataform.Domain.ViewModel.Pessoa;

namespace WebZi.Plataform.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class PessoaController : ControllerBase
{
    private readonly IServiceProvider _provider;

    public PessoaController(IServiceProvider provider)
    {
        _provider = provider;
    }

    [HttpGet("ListarTipoDocumentoIdentificacao")]
    public async Task<ActionResult<TipoDocumentoIdentificacaoListDTO>> ListarTipoDocumentoIdentificacao(
        bool FlagAtivo, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        TipoDocumentoIdentificacaoListDTO ResultView = new();

        try
        {
            ResultView = await _provider
                .GetService<PessoaService>()
                .ListTipoDocumentoIdentificacaoAsync(FlagAtivo, ct);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }

    [HttpGet("ListarTipoDocumentoIdentificacaoSimplificado")]
    // TODO: [Authorize]
    public async Task<ActionResult<TipoDocumentoIdentificacaoSimplificadoListDTO>>
        ListarTipoDocumentoIdentificacaoSimplificado(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        TipoDocumentoIdentificacaoSimplificadoListDTO ResultView = new();

        try
        {
            ResultView = await _provider
                .GetService<PessoaService>()
                .ListTipoDocumentoIdentificacaoSimplificadoAsync(ct);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }


    [HttpPost("ConsultarPessoa")]
    public async Task<ActionResult<PessoaListDTO>> ConsultarPessoa(ConsultaPessoaParameters request,
        CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        PessoaListDTO ResultView = new();
        var usuarioId = User.GetUserId();
        try
        {
            ResultView = await _provider
                .GetService<PessoaService>()
                .ConsultarPessoa(usuarioId!.Value, request, ct);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }
}
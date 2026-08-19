using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Pessoa;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.Pessoa;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.WebServices.Nfe;
using WebZi.Plataform.Domain.DTO.WebServices.Nfse;
using WebZi.Plataform.Domain.ViewModel.NFe;

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

    [HttpGet("ConsultaNfe")]
    public async Task<ActionResult<NFERetornoFaturamentoDTOList>> ConsultarNfe(int grvId, int usuarioId, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        NFERetornoFaturamentoDTOList ResultView = new();

        try
        {
            ResultView = await _provider
                .GetService<WSNfseService>()
                .ConsultarNfeAsync(grvId, usuarioId, ct);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }
    
    [HttpPost("ReprocessarNota")]
    public async Task<ActionResult<MensagemDTO>> ReprocessarNota(int grvId, string notaId, int usuarioId, CancellationToken ct)
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
                .ReprocessNfseAsync(grvId, notaId, usuarioId, ct);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    [HttpPost("GerarNota")]
    public async Task<ActionResult<MensagemDTO>> GerarNota(int grvId, int usuarioId, int? faturamentoId = null, CancellationToken ct = default)
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
                .CreateNfseAsync(grvId, usuarioId, faturamentoId, ct);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }
    [HttpGet("ConsultarJsonNfe")]
    public async Task<ActionResult<NfeJsonEnvioDTO>> ConsultarJsonNfe(long nfeid, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        NfeJsonEnvioDTO ResultView = new();

        try
        {
            ResultView = await _provider
                .GetService<WSNfseService>()
                .GetJsonNfeAsync(nfeid, ct);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }
    
    [HttpPut("AtualizarDadosNotaFiscal")]
    public async Task<ActionResult<MensagemDTO>> AtualizarDadosNotaFiscal(AtualizarDadosNFeParameters parameters, CancellationToken ct)
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
                .UpdateNFeAsync(parameters, ct);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }
}
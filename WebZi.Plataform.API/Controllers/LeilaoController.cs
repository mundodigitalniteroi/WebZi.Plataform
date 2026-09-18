using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Data.Services.Vistorias;
using WebZi.Plataform.Domain.DTO.Leilao;
using WebZi.Plataform.Domain.DTO.Leilao.Vistoria;
using WebZi.Plataform.Domain.DTO.Report;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.Leilao;
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

    [HttpGet("SelecionarVistoria")]
    public async Task<ActionResult<SelecionarVistoriaPreLeilaoDTO>> SelecionarVistoria(
        int identificadorCliente,
        int identificadorEmpresaVistoria,
        [Required(ErrorMessage = "Propriedade obrigatória")]
        int identificadorProcesso,
        string numeroProcesso)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        SelecionarVistoriaPreLeilaoDTO ResultView = new();

        try
        {
            ResultView = await _provider.GetService<VistoriaService>().GetVistoriaAsync(identificadorCliente,
                identificadorEmpresaVistoria, identificadorProcesso, numeroProcesso);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }


    [HttpPatch("IngressarLote")]
    public async Task<ActionResult<MensagemDTO>> IngressarLote(IngressarLoteParameters parameters, CancellationToken ct)
    {


        MensagemDTO ResultView = new();
        if (!ModelState.IsValid)
        {
            return BadRequest(ResultView);
        }


        var userId = User.GetUserId();
        try
        {
            ResultView = await _provider.GetService<LeilaoService>().ChangeStatusPreLeilaoAsync(parameters, userId!.Value, ct);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }


    [HttpPost("CadastrarArrematante")]
    public async Task<ActionResult<MensagemDTO>> CadastrarArrematante(CadastrarArrematanteParameters parameters, CancellationToken ct)
    {
        MensagemDTO ResultView = new();
        try
        {
            ResultView = await _provider.GetService<LeilaoService>().CadastrarArrematanteAsync(parameters, ct);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    [HttpPut("AtualizarArrematante")]
    public async Task<ActionResult<MensagemDTO>> AtualizarArrematante(AtualizarArrematanteParameters parameters, CancellationToken ct)
    {
        MensagemDTO ResultView = new();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.GetUserId();
        try
        {
            ResultView = await _provider.GetService<LeilaoService>().AtualizarArrematanteAsync(parameters, userId, ct);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    [HttpDelete("DesvincularArrematante/{identificadorArrematante}")]
    public async Task<ActionResult<MensagemDTO>> DesvincularArrematante(int identificadorArrematante, CancellationToken ct)
    {
        MensagemDTO ResultView = new();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.GetUserId();
        try
        {
            ResultView = await _provider.GetService<LeilaoService>().DesvincularArrematanteAsync(identificadorArrematante, userId, ct);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    [HttpPatch("DesvincularGrvDoLeilao/{identificadorProcesso}")]
    public async Task<ActionResult<MensagemDTO>> DesvincularGrvDoLeilao(int identificadorProcesso, CancellationToken ct)
    {
        MensagemDTO ResultView = new();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.GetUserId();
        try
        {
            ResultView = await _provider.GetService<LeilaoService>().DesvincularGrvDoLeilaoAsync(identificadorProcesso, userId, ct);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    [HttpGet("SelecionarArrematantePorProcesso")]
    public async Task<ActionResult<SelecionarArrematanteDTO>> SelecionarArrematantePorProcesso(int identificadorProcesso, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        SelecionarArrematanteDTO ResultView = new();
        try
        {
            ResultView = await _provider.GetService<LeilaoService>().SelecionarArrematantePorProcessoAsync(identificadorProcesso, ct);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }

    [HttpGet("DeclaracaoRetiradaVeiculoLeiloado")]
    public async Task<ActionResult<GuiaDeclaracaoRetiradaLeilaoDTO>> DeclaracaoRetiradaVeiculoLeiloado(int identificadorProcesso, CancellationToken ct)
    {
        GuiaDeclaracaoRetiradaLeilaoDTO ResultView = new();
        if (!ModelState.IsValid)
        {
            return BadRequest(ResultView);
        }


        var userId = User.GetUserId();
        try
        {
            ResultView = await _provider.GetService<LeilaoService>().CreateDeclaracaoRetirada(identificadorProcesso, userId!.Value, ct);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }
}

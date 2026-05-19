using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Data.Services.Leilao;
using WebZi.Plataform.Data.Services.Vistorias;
using WebZi.Plataform.Domain.DTO.Leilao;
using WebZi.Plataform.Domain.DTO.Leilao.Vistoria;
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
}
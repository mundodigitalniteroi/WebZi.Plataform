using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.ViewModel.Transalvador.Entrada;

namespace WebZi.Plataform.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TransalvadorController : ControllerBase
{
    private readonly IServiceProvider _provider;

    public TransalvadorController(IServiceProvider provider)
    {
        _provider = provider;
    }


    /// <summary>
    /// Reenvia os dados de um veículo apreendido para a integração com a Transalvador.
    /// </summary>
    /// <remarks>
    /// Realiza o reenvio das informações de entrada/guarda de um veículo apreendido para o sistema externo da Transalvador.
    ///
    /// **Parâmetros do corpo da requisição (Body):**
    /// - `identificadorCliente` (int, obrigatório): Identificador do cliente associado ao processo (deve pertencer à integração Transalvador).
    /// - `identificadorDeposito` (int, obrigatório): Identificador do depósito onde o veículo está alocado.
    /// - `numeroProcesso` (string, obrigatório, máx. 15 caracteres): Número do formulário GRV/processo do veículo apreendido.
    ///
    /// *Nota:* O identificador do usuário (`identificadorUsuario`) é ignorado no corpo da requisição e obtido automaticamente a partir do token de autenticação.
    /// </remarks>
    /// <param name="parameters">Parâmetros com cliente, depósito e número do processo necessários para reenviar o veículo.</param>
    /// <param name="ct">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Objeto <see cref="MensagemDTO"/> com status da operação e mensagem informativa.</returns>
    /// <response code="200">Veículo reenviado para a Transalvador com sucesso.</response>
    /// <response code="400">Dados inválidos, cliente não pertencente à integração, processo sem data de guarda ou veículo já registrado.</response>
    /// <response code="404">GRV/Processo não encontrado para o cliente e depósito informados.</response>
    /// <response code="500">Erro interno do servidor durante o processamento da operação.</response>
    [HttpPost("ReenviarVeiculoApreendido")]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MensagemDTO>> ReenviarVeiculoApreendido(
        [FromBody] ReenviarVeiculoApreendidoParameters parameters,
        CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        MensagemDTO ResultView;

        try
        {
            int? userId = User.GetUserId();
            if (userId.HasValue)
            {
                parameters.identificadorUsuario = userId.Value;
            }

            ResultView = await _provider
                .GetRequiredService<GgvService>()
                .ReenviarVeiculoTransalvadorAsync(parameters, ct);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception ex)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(ex);

            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }
}
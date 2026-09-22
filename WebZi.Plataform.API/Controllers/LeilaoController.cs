using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

/// <summary>
/// Controller responsável pela gestão e operacionalização de processos de Pré-Leilão e Leilão.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class LeilaoController : ControllerBase
{
    private readonly IServiceProvider _provider;

    public LeilaoController(IServiceProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Lista os processos aptos para a esteira de pré-leilão / leilão conforme os filtros fornecidos.
    /// </summary>
    /// <remarks>
    /// Permite pesquisar processos por cliente, depósito, intervalo de datas (guarda, vistoria ou cadastro),
    /// status de operação, placa, chassi, número de processo e outros filtros cadastrais com paginação.
    /// </remarks>
    /// <param name="parameters">Filtros de consulta e paginação para listagem de processos de pré-leilão.</param>
    /// <returns>Objeto contendo a listagem paginada de processos e dados complementares.</returns>
    /// <response code="200">Listagem de processos de pré-leilão retornada com sucesso.</response>
    /// <response code="400">Parâmetros de consulta inválidos ou inconsistentes.</response>
    /// <response code="500">Erro interno do servidor ao consultar processos de pré-leilão.</response>
    [HttpPost("ListarProcessosPreLeilao")]
    [ProducesResponseType(typeof(PreLeilaoListDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PreLeilaoListDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PreLeilaoListDTO), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Consulta os dados e fotos da vistoria de pré-leilão associada a um processo/veículo.
    /// </summary>
    /// <param name="identificadorCliente">Identificador único do cliente (opcional).</param>
    /// <param name="identificadorEmpresaVistoria">Identificador único da empresa de vistoria (opcional).</param>
    /// <param name="identificadorProcesso">Identificador único do processo (obrigatório).</param>
    /// <param name="numeroProcesso">Número do formulário do processo / GRV (opcional).</param>
    /// <returns>Dados detalhados da vistoria de pré-leilão.</returns>
    /// <response code="200">Vistoria encontrada e retornada com sucesso.</response>
    /// <response code="400">Parâmetros de requisição inválidos.</response>
    /// <response code="404">Vistoria não encontrada para os parâmetros informados.</response>
    /// <response code="500">Erro interno do servidor ao consultar a vistoria.</response>
    [HttpGet("SelecionarVistoria")]
    [ProducesResponseType(typeof(SelecionarVistoriaPreLeilaoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SelecionarVistoriaPreLeilaoDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SelecionarVistoriaPreLeilaoDTO), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(SelecionarVistoriaPreLeilaoDTO), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Ingressa uma lista de processos (GRVs) na esteira de pré-leilão.
    /// </summary>
    /// <remarks>
    /// Altera o status da operação dos processos informados para "1" (Pré-Leilão).
    /// Requer que os processos estejam no status "V" (Aguardando Vistoria/Pátio) para serem inseridos.
    /// </remarks>
    /// <param name="parameters">Objeto contendo a lista de IDs de processo ou números de processo a serem ingressados.</param>
    /// <param name="ct">Token de cancelamento da operação assíncrona.</param>
    /// <returns>Resultado da operação com mensagem informativa e status HTTP.</returns>
    /// <response code="200">Processos inseridos no pré-leilão com sucesso.</response>
    /// <response code="400">Nenhum processo informado, processos inexistentes, status incompatível ou já em pré-leilão.</response>
    /// <response code="500">Erro interno do servidor ao ingressar processos em pré-leilão.</response>
    [HttpPatch("IngressarLote")]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Cadastra os dados do arrematante e as informações do leilão para um processo/veículo.
    /// </summary>
    /// <remarks>
    /// Vincula o arrematante ao processo da GRV, definindo endereço, contatos, lote, valores de arrematação,
    /// comissões e taxas. Altera o status da operação do processo para "3" (Leiloado).
    /// </remarks>
    /// <param name="parameters">Dados cadastrais do arrematante e informações do lote arrematado.</param>
    /// <param name="ct">Token de cancelamento da operação assíncrona.</param>
    /// <returns>Resultado do cadastro com mensagem de sucesso ou falha.</returns>
    /// <response code="201">Arrematante cadastrado e vinculado ao processo com sucesso.</response>
    /// <response code="400">Processo não encontrado, status inválido para cadastro ou data do leilão anterior à guarda.</response>
    /// <response code="500">Erro interno do servidor ao cadastrar o arrematante.</response>
    [HttpPost("CadastrarArrematante")]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Atualiza os dados cadastrais, endereço e valores de um arrematante existente.
    /// </summary>
    /// <param name="parameters">Dados cadastrais atualizados do arrematante e identificador de vínculo.</param>
    /// <param name="ct">Token de cancelamento da operação assíncrona.</param>
    /// <returns>Resultado da atualização com mensagem descritiva.</returns>
    /// <response code="200">Dados do arrematante atualizados com sucesso.</response>
    /// <response code="400">Parâmetros inválidos ou erro no processamento da atualização.</response>
    /// <response code="404">Arrematante não encontrado.</response>
    /// <response code="500">Erro interno do servidor ao atualizar o arrematante.</response>
    [HttpPut("AtualizarArrematante")]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Remove o cadastro do arrematante e desvincula-o do processo (GRV).
    /// </summary>
    /// <remarks>
    /// Exclui o registro do arrematante e, caso a GRV esteja no status "3" (Leiloado) ou "6" (Leiloado - Aguardando Entrega), retorna o status da operação para "1" (Pré-Leilão).
    /// </remarks>
    /// <param name="identificadorArrematante">Identificador único do arrematante.</param>
    /// <param name="ct">Token de cancelamento da operação assíncrona.</param>
    /// <returns>Resultado da exclusão e desvinculação.</returns>
    /// <response code="200">Arrematante desvinculado e excluído com sucesso.</response>
    /// <response code="400">Requisição inválida ou erro no processamento.</response>
    /// <response code="404">Arrematante não encontrado.</response>
    /// <response code="500">Erro interno do servidor ao desvincular o arrematante.</response>
    [HttpDelete("{identificadorArrematante}/DesvincularArrematante")]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Remove o processo  do fluxo de leilão e remove qualquer arrematante vinculado.
    /// </summary>
    /// <remarks>
    /// Exclui o arrematante associado ao processo (se existir) e altera o status da operação da GRV para "V".
    /// </remarks>
    /// <param name="identificadorProcesso">Identificador único do processo (GrvId).</param>
    /// <param name="ct">Token de cancelamento da operação assíncrona.</param>
    /// <returns>Resultado da operação com status atualizado.</returns>
    /// <response code="200">Processo desvinculado do leilão com sucesso.</response>
    /// <response code="400">Requisição inválida ou erro no processamento.</response>
    /// <response code="404">Processo não encontrado.</response>
    /// <response code="500">Erro interno do servidor ao desvincular processo do leilão.</response>
    [HttpDelete("{identificadorProcesso}/DesvincularLeilao")]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(MensagemDTO), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MensagemDTO>> DesvincularLeilao(int identificadorProcesso, CancellationToken ct)
    {
        MensagemDTO ResultView = new();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.GetUserId();
        try
        {
            ResultView = await _provider.GetService<LeilaoService>().DesvincularLeilaoAsync(identificadorProcesso, userId, ct);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
        }
    }

    /// <summary>
    /// Obtém os dados completos do arrematante, do veículo e do processo associado.
    /// </summary>
    /// <remarks>
    /// Retorna detalhes cadastrais do arrematante, dados do processo e especificações do veículo.
    /// Válido apenas para processos nos status de operação: "1" (Pré-Leilão), "3" (Leiloado), "6" (Leiloado - Aguardando Entrega) ou "7" (Leiloado - Entregue).
    /// </remarks>
    /// <param name="identificadorProcesso">Identificador único do processo / Numero Processo.</param>
    /// <param name="ct">Token de cancelamento da operação assíncrona.</param>
    /// <returns>Objeto contendo dados do arrematante, veículo e processo.</returns>
    /// <response code="200">Dados do arrematante e veículo retornados com sucesso.</response>
    /// <response code="400">Identificador inválido ou processo em status incompatível para consulta de arrematante.</response>
    /// <response code="404">Processo não encontrado.</response>
    /// <response code="500">Erro interno do servidor ao consultar arrematante por processo.</response>
    [HttpGet("{identificadorProcesso}/SelecionarArrematantePorProcesso")]
    [ProducesResponseType(typeof(SelecionarArrematanteDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SelecionarArrematanteDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SelecionarArrematanteDTO), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(SelecionarArrematanteDTO), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SelecionarArrematanteDTO>> SelecionarArrematantePorProcesso(int identificadorProcesso, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        SelecionarArrematanteDTO ResultView = new();
        try
        {
            ResultView = await _provider.GetService<LeilaoService>().GetArrematantePorProcessoAsync(identificadorProcesso, ct);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }

    /// <summary>
    /// Gera o modelo de dados para emissão da Declaração de Retirada do Veículo Leiloado.
    /// </summary>
    /// <remarks>
    /// Valida as permissões do usuário logado e o status da GRV (exige status "6" ou "7"),
    /// retornando os dados completos do arrematante, veículo, depósito e termos para confecção do documento/PDF.
    /// </remarks>
    /// <param name="identificadorProcesso">Identificador único do processo.</param>
    /// <param name="ct">Token de cancelamento da operação assíncrona.</param>
    /// <returns>Dados completos estruturados para a declaração de retirada do veículo.</returns>
    /// <response code="200">Declaração de retirada gerada com sucesso.</response>
    /// <response code="400">Usuário sem permissão no pátio ou status do processo não permite a geração do documento.</response>
    /// <response code="404">Processo não encontrado.</response>
    /// <response code="500">Erro interno do servidor ao gerar declaração de retirada.</response>
    [HttpGet("{identificadorProcesso}/DeclaracaoRetiradaVeiculoLeiloado")]
    [ProducesResponseType(typeof(GuiaDeclaracaoRetiradaLeilaoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GuiaDeclaracaoRetiradaLeilaoDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GuiaDeclaracaoRetiradaLeilaoDTO), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GuiaDeclaracaoRetiradaLeilaoDTO), StatusCodes.Status500InternalServerError)]
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
            ResultView = await _provider.GetService<LeilaoService>().CreateDeclaracaoRetiradaAsync(identificadorProcesso, userId!.Value, ct);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
        catch (Exception e)
        {
            ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(e);
            return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
        }
    }
}

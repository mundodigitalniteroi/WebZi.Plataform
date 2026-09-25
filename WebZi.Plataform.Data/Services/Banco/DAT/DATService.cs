using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Transalvador;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Transalvador.DAT.Gerar;
using Z.EntityFramework.Plus;
using static System.Int32;

namespace WebZi.Plataform.Data.Services.Banco.DAT;

public class DATService
{
    private readonly AppDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IServiceProvider _provider;

    public DATService(AppDbContext context, IHttpClientFactory httpClientFactory, IServiceProvider provider)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
        _provider = provider;
    }

    public async Task<DATDTO> GerarAsync(int faturamentoId, CancellationToken ct = default)
    {
        DATDTO ResultView = new();

        var faturamento = await _context.Faturamento
            .AsTracking()
            .Where(x => x.FaturamentoId == faturamentoId && x.Status != "C")
            .OrderBy(x => x.DataCadastro)
            .Select(x => new
            {
                x.FaturamentoId,
                x.AtendimentoId,
                x.ValorFaturado,
                x.DataVencimento,
                x.UsuarioCadastroId,
                NumeroProcesso = x.Atendimento.Grv.NumeroFormularioGrv,
                ResponsavelNome = x.Atendimento.ResponsavelNome,
                ResponsavelDocumento = x.Atendimento.ResponsavelDocumento,
                NotaFiscalEmail = x.Atendimento.NotaFiscalEmail,
                IdEntradaTransalvador = x.Atendimento.Grv.IdEntradaTransalvador,
                ReceitaId = x.Atendimento.Grv.Deposito.ClientesDepositos
                    .Where(cd => cd.ClienteId == x.Atendimento.Grv.ClienteId && cd.FlagAtivo == "S")
                    .Select(cd => cd.SistemaExternoId)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync(ct);
        if (faturamento == null)
        {
            ResultView.Mensagem =
                MensagemViewHelper.SetNotFound("Faturamento não encontrado.");
            return ResultView;
        }

        if (faturamento.IdEntradaTransalvador == null)
        {
            ResultView.Mensagem =
                MensagemViewHelper.SetBadRequest(
                    "O veículo não possui o ID de entrada na Transalvador.");
            return ResultView;
        }

        var parsed = TryParse(faturamento.ReceitaId, out var receitaId);

        if (!parsed)
        {
            ResultView.Mensagem =
                MensagemViewHelper.SetBadRequest(
                    "Erro ao tentar tratar Identificador Sistema Externo.");
            return ResultView;
        }

        string? dataVencimento = null;
        if (faturamento.DataVencimento > DateTime.MinValue)
        {
            DateTime dataAjustada = faturamento.DataVencimento.Date >= DateTime.Today
                ? faturamento.DataVencimento.Date
                : DateTime.Today;

            dataVencimento = dataAjustada.ToString("yyyy-MM-dd");
        }

        GerarDATParameters parameters = new()
        {
            IdVpa = faturamento.IdEntradaTransalvador.Value.ToString(),
            NomeRequerente = faturamento.ResponsavelNome,
            Cpf = faturamento.ResponsavelDocumento,
            Email = string.IsNullOrWhiteSpace(faturamento.NotaFiscalEmail) ? null : faturamento.NotaFiscalEmail,
            Valor = faturamento.ValorFaturado,
            ReceitaId = receitaId,
            DataVencimento = dataVencimento,
        };

        var result = await _provider.GetRequiredService<TransalvadorService>().GerarDATAsync(parameters, ct);

        if (result.Mensagem.HtmlStatusCode is HtmlStatusCodeEnum.Ok or HtmlStatusCodeEnum.Created)
        {
            await _context.Faturamento
                .Where(x => x.FaturamentoId == faturamento.FaturamentoId)
                .UpdateAsync(x => new FaturamentoModel()
                {
                    NumeroDocumentoDat = result.NumeroDocumentoDat,
                    DataAlteracao = DateTime.Now
                }, ct);

            result.IdentificadorAtendimento = faturamento.AtendimentoId;
            result.IdentificadorFaturamento = faturamento.FaturamentoId;
            result.NumeroProcesso = faturamento.NumeroProcesso;
        }

        return result;
    }

    public async Task<DATDTO> ConsultarAsync(int faturamentoId, CancellationToken ct = default)
    {
        DATDTO ResultView = new();

        var faturamento = await _context.Faturamento
            .AsNoTracking()
            .Where(x => x.FaturamentoId == faturamentoId && x.Status != "C")
            .OrderByDescending(x => x.DataCadastro)
            .Select(x => new
            {
                x.AtendimentoId,
                x.FaturamentoId,
                x.NumeroDocumentoDat,
                NumeroProcesso = x.Atendimento.Grv.NumeroFormularioGrv,
            })
            .FirstOrDefaultAsync(ct);

        if (faturamento == null)
        {
            ResultView.Mensagem =
                MensagemViewHelper.SetNotFound("Faturamento não encontrado.");
            return ResultView;
        }

        if (string.IsNullOrWhiteSpace(faturamento.NumeroDocumentoDat))
        {
            ResultView.Mensagem =
                MensagemViewHelper.SetBadRequest("O faturamento não possui número de DAT emitido para consulta.");
            return ResultView;
        }

        var result = await _provider.GetRequiredService<TransalvadorService>()
            .EmitirSegundaViaAsync(faturamento.NumeroDocumentoDat, ct);

        if (result.Mensagem.HtmlStatusCode is HtmlStatusCodeEnum.Ok or HtmlStatusCodeEnum.Created)
        {
            result.IdentificadorAtendimento = faturamento.AtendimentoId;
            result.IdentificadorFaturamento = faturamento.FaturamentoId;
            result.NumeroProcesso = faturamento.NumeroProcesso;
        }

        return result;
    }
}
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Transalvador;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.ViewModel.Transalvador.DAT.Gerar;
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

        GerarDATParameters parameters = new()
        {
            IdVpa = faturamento.IdEntradaTransalvador.Value.ToString(),
            NomeRequerente = faturamento.ResponsavelNome,
            Cpf = faturamento.ResponsavelDocumento,
            Email = string.IsNullOrWhiteSpace(faturamento.NotaFiscalEmail) ? null : faturamento.NotaFiscalEmail,
            Valor = faturamento.ValorFaturado,
            ReceitaId = receitaId,
            DataVencimento = faturamento.DataVencimento > DateTime.MinValue || faturamento.DataVencimento == null
                ? faturamento.DataVencimento.ToString("dd/MM/yyyy")
                : null,
        };

        var result = await _provider.GetRequiredService<TransalvadorService>().GerarDATAsync(parameters, ct);

        if (result.Mensagem.HtmlStatusCode is HtmlStatusCodeEnum.Ok or HtmlStatusCodeEnum.Created)
        {
            await _context.Faturamento
                .Where(x => x.FaturamentoId == faturamento.FaturamentoId)
                .ExecuteUpdateAsync(s => s.SetProperty(f => f.NumeroDocumentoDat, result.NumeroDocumentoDat), ct);

            result.IdentificadorAtendimento = faturamento.AtendimentoId;
            result.IdentificadorFaturamento = faturamento.FaturamentoId;
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
                x.NumeroDocumentoDat
            })
            .FirstOrDefaultAsync(ct);

        if (faturamento == null)
        {
            ResultView.Mensagem =
                MensagemViewHelper.SetNotFound("Faturamento não encontrado informado.");
            return ResultView;
        }


        var result = await _provider.GetRequiredService<TransalvadorService>()
            .EmitirSegundaViaAsync(faturamento.NumeroDocumentoDat, ct);

        if (result.Mensagem.HtmlStatusCode is HtmlStatusCodeEnum.Ok or HtmlStatusCodeEnum.Created)
        {
            result.IdentificadorAtendimento = faturamento.AtendimentoId;
            result.IdentificadorFaturamento = faturamento.FaturamentoId;
        }

        return result;
    }
}
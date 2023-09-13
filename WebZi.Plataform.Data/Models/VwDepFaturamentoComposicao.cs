using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepFaturamentoComposicao
{
    public int IdGrv { get; set; }

    public string IdStatusOperacao { get; set; }

    public int IdAtendimento { get; set; }

    public int IdFaturamento { get; set; }

    public int IdFaturamentoComposicao { get; set; }

    public int? IdFaturamentoServicoTipoVeiculo { get; set; }

    public byte? IdFaturamentoTipoComposicao { get; set; }

    public int? IdUsuarioDesconto { get; set; }

    public string IdDocumentoSap { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string StatusCadastroSap { get; set; }

    public string StatusCadastroOrdensVendaSap { get; set; }

    public string FaturamentoStatus { get; set; }

    public string TipoComposicao { get; set; }

    public decimal ValorTipoComposicao { get; set; }

    public decimal? QuantidadeComposicao { get; set; }

    public decimal ValorComposicao { get; set; }

    public string TipoDesconto { get; set; }

    public int? QuantidadeDesconto { get; set; }

    public decimal? ValorDesconto { get; set; }

    public string ObservacaoDesconto { get; set; }

    public decimal ValorFaturado { get; set; }

    public int IdFaturamentoServicoTipo { get; set; }

    public string ServicosTiposDescricao { get; set; }

    public string ServicosTiposTipoCobranca { get; set; }

    public byte ServicosTiposOrdemImpressao { get; set; }

    public string ServicosTiposFlagCobrarTelaGrv { get; set; }

    public string ServicosTiposFlagNaoCobrarSeNaoUsouReboque { get; set; }

    public string ServicosTiposFlagServicoObrigatorio { get; set; }

    public string ServicosTiposFlagRebocada { get; set; }

    public string ServicosTiposFlagImpressaoAgrupada { get; set; }

    public string ServicosTiposFlagTributacao { get; set; }

    public string ServicosTiposFlagCobrancaPorHora { get; set; }

    public string ServicosTiposFlagAtivo { get; set; }

    public string SapTipoComposicaoCodigoMaterial { get; set; }

    public string SapTipoComposicaoTipoDocumentoVenda { get; set; }

    public string SapTipoComposicaoDescricaoNotaFiscal { get; set; }

    public int? CnaeId { get; set; }

    public string CnaeCodigo { get; set; }

    public string CnaeCodigoFormatado { get; set; }

    public string CnaeDescricao { get; set; }

    public int? ListaServicoId { get; set; }

    public string ListaServicoItemLista { get; set; }

    public string ListaServicoDescricao { get; set; }

    public decimal? ListaServicoAliquotaIss { get; set; }
}

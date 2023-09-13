using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogFaturamentoComposicao
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int? IdFaturamentoComposicao { get; set; }

    public int? IdFaturamento { get; set; }

    public int? IdFaturamentoServicoTipoVeiculo { get; set; }

    public int? IdFaturamentoTipoComposicao { get; set; }

    public int? IdUsuarioDesconto { get; set; }

    public string IdDocumentoSap { get; set; }

    public string TipoLancamento { get; set; }

    public string TipoComposicao { get; set; }

    public decimal? ValorTipoComposicao { get; set; }

    public decimal? QuantidadeComposicao { get; set; }

    public decimal? ValorComposicao { get; set; }

    public string TipoDesconto { get; set; }

    public int? QuantidadeDesconto { get; set; }

    public decimal? ValorDesconto { get; set; }

    public string ObservacaoDesconto { get; set; }

    public decimal ValorFaturado { get; set; }

    public int? IdUsuarioAlteracaoQuantidade { get; set; }

    public decimal? QuantidadeAlterada { get; set; }

    public string ObservacaoQuantidadeAlterada { get; set; }
}

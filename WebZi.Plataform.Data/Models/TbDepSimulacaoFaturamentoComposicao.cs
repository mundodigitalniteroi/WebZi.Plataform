using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSimulacaoFaturamentoComposicao
{
    public int IdGrv { get; set; }

    public int? IdFaturamentoServicoTipoVeiculo { get; set; }

    public string TipoComposicao { get; set; }

    public double? ValorTipoComposicao { get; set; }

    public double? QuantidadeComposicao { get; set; }

    public double? ValorComposicao { get; set; }

    public string ServicoDescricao { get; set; }

    public DateTime? DataHora { get; set; }
}

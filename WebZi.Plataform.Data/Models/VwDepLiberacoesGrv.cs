using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepLiberacoesGrv
{
    public int IdDeposito { get; set; }

    public string NomeDeposito { get; set; }

    public int IdCliente { get; set; }

    public string NomeCliente { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string DescricaoServico { get; set; }

    public decimal ValorTipoComposicao { get; set; }

    public int QuantidadeComposicao { get; set; }

    public DateTime? DataPagamento { get; set; }

    public int IdTarifaTipoVeiculo { get; set; }

    public int? IdReboquista { get; set; }

    public int? IdReboque { get; set; }

    public int? IdAutoridadeResponsavel { get; set; }
}

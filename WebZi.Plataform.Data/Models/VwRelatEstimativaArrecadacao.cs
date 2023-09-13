using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwRelatEstimativaArrecadacao
{
    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string TipoVeiculosNome { get; set; }

    public string TarifaDescricao { get; set; }

    public decimal TarifaPrecoDiaria { get; set; }

    public int? Dias { get; set; }

    public decimal TarifaPrecoRebocada { get; set; }

    public string FlagComboio { get; set; }

    public DateTime? DataHoraRemocao { get; set; }

    public decimal? TotalDiaria { get; set; }

    public decimal? TotalDevido { get; set; }

    public string Deposito { get; set; }

    public string Cliente { get; set; }

    public int? IdAutoridadeResponsavel { get; set; }

    public string Autoridade { get; set; }
}

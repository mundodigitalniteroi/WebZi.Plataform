using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwVeiculosLiberado
{
    public string NumeroFormularioGrv { get; set; }

    public DateTime? DataHoraRemocao { get; set; }

    public DateTime? DataHoraGuarda { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string MarcaModelo { get; set; }

    public string TipoVeiculo { get; set; }

    public string Cor { get; set; }

    public DateTime? DataPagamento { get; set; }

    public string Status { get; set; }

    public string Tarifa { get; set; }

    public int Diarias { get; set; }

    public decimal ValorFaturado { get; set; }

    public string NumeroNotaFiscal { get; set; }

    public DateTime? DataEmissaoNotaFiscal { get; set; }
}

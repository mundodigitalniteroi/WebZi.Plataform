using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwLeilaoBoletosPago
{
    public int IdBoleto { get; set; }

    public string NossoNumero { get; set; }

    public DateTime DataCadastroBoleto { get; set; }

    public DateTime? DataVencimento { get; set; }

    public DateTime? DataCredito { get; set; }

    public string SacadoCpfCnpj { get; set; }

    public string SacadoNome { get; set; }

    public decimal? RetornoBancarioCef { get; set; }

    public decimal? ValorLiquidoAserCreditado { get; set; }

    public string Lote { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepFaturamento
{
    public int IdGrv { get; set; }

    public string IdStatusOperacao { get; set; }

    public int IdAtendimento { get; set; }

    public int IdFaturamento { get; set; }

    public string IdDocumentoSap { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string StatusCadastroSap { get; set; }

    public string StatusCadastroOrdensVendaSap { get; set; }

    public decimal ValorFaturado { get; set; }

    public decimal? ValorPagamento { get; set; }

    public DateTime DataVencimento { get; set; }

    public DateTime? DataPagamento { get; set; }

    public DateTime? DataPrazoRetiradaVeiculo { get; set; }

    public DateTime? DataEmissaoDocumento { get; set; }

    public DateTime? DataEmissaoNotaFiscal { get; set; }

    public string Status { get; set; }
}

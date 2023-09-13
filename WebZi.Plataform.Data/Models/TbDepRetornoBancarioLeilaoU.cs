using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepRetornoBancarioLeilaoU
{
    public int IdRetornoBancarioLeilaoU { get; set; }

    public int? CodigoBanco { get; set; }

    public string LoteServico { get; set; }

    public int? CodigoRegistro { get; set; }

    public int? NumeroRegistro { get; set; }

    public string CodigoSegmento { get; set; }

    public int? CodigoMovimento { get; set; }

    public string JurosMultaEncargos { get; set; }

    public decimal? ValorDescontoConcedido { get; set; }

    public string ValorAbatimentoConcedido { get; set; }

    public decimal? ValorIofRecolhido { get; set; }

    public decimal? ValorPagoPeloSacado { get; set; }

    public decimal? ValorLiquidoAserCreditado { get; set; }

    public decimal? ValorOutrasDespesas { get; set; }

    public decimal? ValorOutrosCreditos { get; set; }

    public DateTime? DataOcorrencia { get; set; }

    public DateTime? DataPagamento { get; set; }

    public DateTime? DataLiquidacao { get; set; }

    public DateTime? DataCredito { get; set; }

    public DateTime? DataDebitotarifa { get; set; }

    public string CodigoSacadoBanco { get; set; }

    public int? CodigoBancoCompensacao { get; set; }

    public string NossoNumeroBancoCompensacao { get; set; }

    public string IdArquivo { get; set; }

    public int? NumeroBancoSacados { get; set; }

    public string NomeBancoSacados { get; set; }

    public string IdAjusteVencimento { get; set; }

    public string IdAjusteEmissao { get; set; }

    public string IdModeloBloqueto { get; set; }

    public string IdViaEntregaDistribuicao { get; set; }

    public string IdEspecieTitulo { get; set; }

    public string IdAceite { get; set; }
}

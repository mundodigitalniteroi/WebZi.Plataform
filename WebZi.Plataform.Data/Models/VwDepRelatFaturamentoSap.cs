using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepRelatFaturamentoSap
{
    public int IdAtendimento { get; set; }

    public string CodigoMaterial { get; set; }

    public string TipoDocumentoVenda { get; set; }

    public string DescricaoNotaFiscal { get; set; }

    public decimal? QuantidadeComposicao { get; set; }

    public decimal? ValorComposicao { get; set; }

    public decimal? ValorDesconto { get; set; }

    public string IdDocumentoSap { get; set; }

    public string Processo { get; set; }

    public string DataLiberacao { get; set; }

    public string Documento { get; set; }

    public string CodigoClienteSap { get; set; }
}

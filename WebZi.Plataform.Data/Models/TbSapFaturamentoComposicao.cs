using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbSapFaturamentoComposicao
{
    public int IdSapFaturamentoComposicao { get; set; }

    public int IdTransacaoSap { get; set; }

    public int IdAtendimento { get; set; }

    public string TipoComposicao { get; set; }

    public string TipoDocumento { get; set; }

    public string Centro { get; set; }

    public string NumeroContrato { get; set; }

    public string CodigoCliente { get; set; }

    public string CodigoPedido { get; set; }

    public string TextoCabecalho { get; set; }

    public string NumeroProcesso { get; set; }

    public string Periodo { get; set; }

    public string NumeroLeilaoPatioLote { get; set; }

    public string CodigoMaterial { get; set; }

    public decimal? Quantidade { get; set; }

    public decimal? ValorBruto { get; set; }

    public decimal? ValorDesconto { get; set; }

    public string CodigoOrdem { get; set; }

    public string MeioPagamento { get; set; }

    public string NumeroDocumento { get; set; }

    public DateTime DataCadastro { get; set; }

    public string FlagErroWsSap { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepPix
{
    public int PixId { get; set; }

    public int FaturamentoId { get; set; }

    public string Chave { get; set; }

    public string SolicitacaoPagador { get; set; }

    public string InfoAdicionais { get; set; }

    public decimal Valor { get; set; }

    public string MerchantName { get; set; }

    public string MerchantCity { get; set; }

    public string Qrstring { get; set; }

    public string Qrcode { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepFaturamento Faturamento { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbFaturaComposicaoCobranca
{
    public int Id { get; set; }

    public int? IdFatura { get; set; }

    public int? IdFormaPagamento { get; set; }

    public string IdReferencia { get; set; }

    public decimal? Valor { get; set; }

    public decimal? ValorTaxa { get; set; }

    public string Qrstring { get; set; }

    public string Qrcode { get; set; }

    public virtual TbArrematantesFatura IdFaturaNavigation { get; set; }

    public virtual TbFaturaFormaPagamento IdFormaPagamentoNavigation { get; set; }
}

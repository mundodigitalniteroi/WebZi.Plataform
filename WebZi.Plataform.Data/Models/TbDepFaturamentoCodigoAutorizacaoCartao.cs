using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoCodigoAutorizacaoCartao
{
    public int IdFaturamentoCodigoAutorizacaoCartao { get; set; }

    public int IdFaturamento { get; set; }

    public byte IdCartao { get; set; }

    public string CodigoAutorizacaoCartao { get; set; }

    public decimal Valor { get; set; }

    public string NumeroCartao { get; set; }

    public virtual TbDepFaturamento IdFaturamentoNavigation { get; set; }
}

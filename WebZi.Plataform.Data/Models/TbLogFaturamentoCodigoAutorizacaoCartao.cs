using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogFaturamentoCodigoAutorizacaoCartao
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int? IdFaturamentoCodigoAutorizacaoCartao { get; set; }

    public int? IdFaturamento { get; set; }

    public byte IdCartao { get; set; }

    public string CodigoAutorizacaoCartao { get; set; }

    public decimal Valor { get; set; }

    public string NumeroCartao { get; set; }
}

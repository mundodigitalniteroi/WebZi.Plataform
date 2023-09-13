using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoCartao
{
    public int IdFaturamentoCartao { get; set; }

    public int IdFaturamento { get; set; }

    public string ReferenceId { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public decimal Valor { get; set; }

    public DateTime DataIntencao { get; set; }

    public DateTime DataExpiration { get; set; }
}

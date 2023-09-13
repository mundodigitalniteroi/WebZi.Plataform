using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoBoletosBak
{
    public int IdFaturamentoBoleto { get; set; }

    public int IdFaturamento { get; set; }

    public int IdBoleto { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public byte? SequenciaEmissao { get; set; }

    public byte? Via { get; set; }

    public byte? DiasConfiguracaoDataVencimento { get; set; }

    public decimal Valor { get; set; }

    public string Linha { get; set; }

    public DateTime? DataEmissao { get; set; }

    public string Status { get; set; }
}

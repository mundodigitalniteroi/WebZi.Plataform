using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepFaturamentoRegra
{
    public short IdFaturamentoRegra { get; set; }

    public short IdFaturamentoRegraTipo { get; set; }

    public int? IdCliente { get; set; }

    public string Cliente { get; set; }

    public int? IdDeposito { get; set; }

    public string Deposito { get; set; }

    public string Valor { get; set; }

    public DateTime DataVigenciaInicial { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }
}

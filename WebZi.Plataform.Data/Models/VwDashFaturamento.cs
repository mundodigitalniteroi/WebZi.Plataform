using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDashFaturamento
{
    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int? Ano { get; set; }

    public int? Mes { get; set; }

    public decimal? Pagamento { get; set; }
}

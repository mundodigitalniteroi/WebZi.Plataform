using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwGrvDigitalManual
{
    public int ValorTotal { get; set; }

    public string Operacao { get; set; }

    public string Deposito { get; set; }

    public DateTime? Data { get; set; }
}

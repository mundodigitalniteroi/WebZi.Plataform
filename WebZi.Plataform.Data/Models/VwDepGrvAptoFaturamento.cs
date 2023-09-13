using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepGrvAptoFaturamento
{
    public int IdGrv { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public string IdStatusOperacao { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Renavam { get; set; }
}

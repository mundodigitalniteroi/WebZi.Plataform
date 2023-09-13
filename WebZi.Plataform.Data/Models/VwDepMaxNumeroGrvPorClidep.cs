using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepMaxNumeroGrvPorClidep
{
    public int ClienteId { get; set; }

    public int DepositoId { get; set; }

    public string Cliente { get; set; }

    public string Depósito { get; set; }

    public string ÚltimoNúmeroFormulárioGrv { get; set; }
}

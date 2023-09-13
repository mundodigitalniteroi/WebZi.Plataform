using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwGrvAptoEmissaoNfe
{
    public int GrvId { get; set; }

    public string NumeroFormulárioGrv { get; set; }

    public string Cliente { get; set; }

    public string Deposito { get; set; }

    public DateTime DataLiberacao { get; set; }
}

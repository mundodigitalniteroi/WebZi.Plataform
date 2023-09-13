using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvEnquadramentoInfraco
{
    public int IdGrvEnquadramentoInfracao { get; set; }

    public int IdGrv { get; set; }

    public decimal IdEnquadramentoInfracao { get; set; }

    public string NumeroInfracao { get; set; }

    public virtual TbDepEnquadramentoInfraco IdEnquadramentoInfracaoNavigation { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }
}

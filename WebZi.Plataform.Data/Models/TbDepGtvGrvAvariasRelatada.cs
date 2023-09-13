using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGtvGrvAvariasRelatada
{
    public int IdGtvGrvAvariaRelatada { get; set; }

    public int IdGtvGrv { get; set; }

    public string RelatoAvaria { get; set; }

    public virtual TbDepGtvGrv IdGtvGrvNavigation { get; set; }
}

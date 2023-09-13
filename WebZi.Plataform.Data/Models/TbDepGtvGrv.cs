using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGtvGrv
{
    public int IdGtvGrv { get; set; }

    public int IdGtv { get; set; }

    public int IdGrv { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepGtv IdGtvNavigation { get; set; }

    public virtual ICollection<TbDepGtvGrvAvariasRelatada> TbDepGtvGrvAvariasRelatada { get; set; } = new List<TbDepGtvGrvAvariasRelatada>();
}

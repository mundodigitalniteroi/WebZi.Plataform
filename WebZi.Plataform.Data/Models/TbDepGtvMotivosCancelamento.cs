using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGtvMotivosCancelamento
{
    public int IdGtvMotivo { get; set; }

    public int IdGtv { get; set; }

    public string Motivo { get; set; }

    public virtual TbDepGtv IdGtvNavigation { get; set; }
}

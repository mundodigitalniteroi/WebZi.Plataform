using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepPreGrvFoto
{
    public int IdPreGrvFoto { get; set; }

    public int IdPreGrv { get; set; }

    public byte[] Foto { get; set; }

    public virtual TbDepPreGrv IdPreGrvNavigation { get; set; }
}

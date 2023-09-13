using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvDrfaTipoRegistro
{
    public byte IdGrvDrfaTipoRegistro { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepGrvDrfa> TbDepGrvDrfas { get; set; } = new List<TbDepGrvDrfa>();

    public virtual ICollection<TbDepSolicitacaoReboqueDrfa> TbDepSolicitacaoReboqueDrfas { get; set; } = new List<TbDepSolicitacaoReboqueDrfa>();
}

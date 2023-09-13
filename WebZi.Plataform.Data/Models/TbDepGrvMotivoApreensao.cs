using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvMotivoApreensao
{
    public byte IdMotivoApreensao { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public string FlagDefault { get; set; }

    public virtual ICollection<TbDepGrv> TbDepGrvs { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepSolicitacaoReboqueTipo> TbDepSolicitacaoReboqueTipos { get; set; } = new List<TbDepSolicitacaoReboqueTipo>();
}

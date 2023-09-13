using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSapTipoComposicaoGrupo
{
    public byte IdSapTipoComposicaoGrupos { get; set; }

    public string Descricao { get; set; }

    public int? IdSapTipoComposicaoMaterialAgrupamento { get; set; }

    public virtual ICollection<TbDepSapTipoComposicao> TbDepSapTipoComposicaos { get; set; } = new List<TbDepSapTipoComposicao>();
}

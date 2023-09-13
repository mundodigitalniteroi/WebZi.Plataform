using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloEmpEmpresasClassificacao
{
    public byte IdEmpresaClassificacao { get; set; }

    public string Descricao { get; set; }

    public string FlagMatriz { get; set; }

    public virtual ICollection<TbGloEmpEmpresa> TbGloEmpEmpresas { get; set; } = new List<TbGloEmpEmpresa>();
}

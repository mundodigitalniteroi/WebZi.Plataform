using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloPesTiposProfisso
{
    public short IdTipoProfissao { get; set; }

    public string Descricao { get; set; }

    public string FlagAtivo { get; set; }

    public virtual ICollection<TbGloPesPessoa> TbGloPesPessoas { get; set; } = new List<TbGloPesPessoa>();
}

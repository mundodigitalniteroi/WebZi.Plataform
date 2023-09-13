using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepLiberacaoEspecialTipo
{
    public byte IdLiberacaoEspecialTipo { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepLiberacaoEspecial> TbDepLiberacaoEspecials { get; set; } = new List<TbDepLiberacaoEspecial>();
}

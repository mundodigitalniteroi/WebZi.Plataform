using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloLocRegio
{
    public string Regiao { get; set; }

    public string Nome { get; set; }

    public virtual ICollection<TbGloLocEstado> TbGloLocEstados { get; set; } = new List<TbGloLocEstado>();
}

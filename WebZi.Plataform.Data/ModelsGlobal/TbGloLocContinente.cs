using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloLocContinente
{
    public string Continente { get; set; }

    public string Nome { get; set; }

    public string NomePtbr { get; set; }

    public virtual ICollection<TbGloLocPaise> TbGloLocPaises { get; set; } = new List<TbGloLocPaise>();
}

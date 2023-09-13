using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloLocPaise
{
    public string PaisNumcode { get; set; }

    public string Continente { get; set; }

    public string Iso { get; set; }

    public string Iso3 { get; set; }

    public string Nome { get; set; }

    public string NomePtbr { get; set; }

    public virtual TbGloLocContinente ContinenteNavigation { get; set; }

    public virtual ICollection<TbGloLocEstado> TbGloLocEstados { get; set; } = new List<TbGloLocEstado>();
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloLocBairro
{
    public int IdBairro { get; set; }

    public int IdMunicipio { get; set; }

    public string Nome { get; set; }

    public string NomePtbr { get; set; }

    public virtual TbGloLocMunicipio IdMunicipioNavigation { get; set; }

    public virtual ICollection<TbGloLocCep> TbGloLocCeps { get; set; } = new List<TbGloLocCep>();
}

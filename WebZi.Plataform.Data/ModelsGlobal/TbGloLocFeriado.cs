using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloLocFeriado
{
    public short IdFeriado { get; set; }

    public string Uf { get; set; }

    public int? IdMunicipio { get; set; }

    public decimal Dia { get; set; }

    public decimal Mes { get; set; }

    public decimal? Ano { get; set; }

    public string Descricao { get; set; }

    public string FlagFeriadoEstadual { get; set; }

    public string FlagFeriadoNacional { get; set; }

    public virtual TbGloLocMunicipio IdMunicipioNavigation { get; set; }

    public virtual TbGloLocEstado UfNavigation { get; set; }
}

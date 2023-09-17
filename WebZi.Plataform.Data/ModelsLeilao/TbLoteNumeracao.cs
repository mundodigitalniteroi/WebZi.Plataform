using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLoteNumeracao
{
    public int? IdLeilao { get; set; }

    public int? PrefixoLote { get; set; }

    public int? UltimoLote { get; set; }
}

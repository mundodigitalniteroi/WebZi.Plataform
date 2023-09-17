using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeiaoPrestacaoConta
{
    public int Id { get; set; }

    public int? IdLeilao { get; set; }

    public int? IdLote { get; set; }

    public int? DiasPatio { get; set; }

    public decimal? ValorArrematacao { get; set; }

    public decimal? CoeficienteLeilao { get; set; }
}

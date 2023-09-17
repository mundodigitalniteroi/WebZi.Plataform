using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbArrematantesFaturaItem
{
    public int Id { get; set; }

    public int? IdFatura { get; set; }

    public int? IdArrematantes { get; set; }

    public string Descricao { get; set; }

    public decimal? ValorUnitario { get; set; }

    public int? Quantidade { get; set; }

    public virtual TbArrematantesFatura IdFaturaNavigation { get; set; }
}

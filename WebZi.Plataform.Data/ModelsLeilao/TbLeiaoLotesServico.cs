using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeiaoLotesServico
{
    public int Id { get; set; }

    public int? IdLeilao { get; set; }

    public int? IdLote { get; set; }

    public string Descricao { get; set; }

    public decimal? Valor { get; set; }

    public int? Ordem { get; set; }
}

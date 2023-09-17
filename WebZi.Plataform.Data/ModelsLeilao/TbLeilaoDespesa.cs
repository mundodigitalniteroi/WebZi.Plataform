using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoDespesa
{
    public int Id { get; set; }

    public int IdLeilao { get; set; }

    public int IdDespesa { get; set; }

    public decimal Valor { get; set; }
}

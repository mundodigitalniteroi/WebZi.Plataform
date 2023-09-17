using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoLotesDespesa
{
    public int Id { get; set; }

    public int IdLote { get; set; }

    public int IdDespesa { get; set; }

    public string Referencia { get; set; }

    public decimal Valor { get; set; }

    public virtual TbLeilaoLote IdLoteNavigation { get; set; }
}

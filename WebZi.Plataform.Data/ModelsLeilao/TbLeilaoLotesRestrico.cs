using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoLotesRestrico
{
    public int Id { get; set; }

    public int IdLote { get; set; }

    public string Codigo { get; set; }

    public string Restricao { get; set; }

    public string SubRestricao { get; set; }

    public string Observacoes { get; set; }

    public string Origem { get; set; }

    public DateTime DataHora { get; set; }

    public virtual TbLeilaoLote IdLoteNavigation { get; set; }
}

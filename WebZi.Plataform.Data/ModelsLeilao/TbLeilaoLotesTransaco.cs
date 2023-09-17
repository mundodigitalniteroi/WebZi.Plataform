using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoLotesTransaco
{
    public int Id { get; set; }

    public int IdLote { get; set; }

    public string Transacao { get; set; }

    public string Retorno { get; set; }

    public DateTime DataHora { get; set; }

    public int? IdUsuario { get; set; }

    public virtual TbLeilaoLote IdLoteNavigation { get; set; }
}

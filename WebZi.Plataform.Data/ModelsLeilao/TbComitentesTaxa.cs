using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbComitentesTaxa
{
    public int Id { get; set; }

    public int IdComitente { get; set; }

    public string Descricao { get; set; }

    public decimal? Valor { get; set; }

    public virtual TbComitente IdComitenteNavigation { get; set; }
}

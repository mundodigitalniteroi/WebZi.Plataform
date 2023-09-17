using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoEditai
{
    public int Id { get; set; }

    public int IdLeilao { get; set; }

    public DateTime DataGeracao { get; set; }

    public int IdUsuarioGeracao { get; set; }

    public virtual TbLeilao IdLeilaoNavigation { get; set; }
}

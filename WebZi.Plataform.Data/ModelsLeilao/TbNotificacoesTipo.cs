using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbNotificacoesTipo
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public string FlgStatus { get; set; }

    public virtual ICollection<TbNotificaco> TbNotificacos { get; set; } = new List<TbNotificaco>();
}

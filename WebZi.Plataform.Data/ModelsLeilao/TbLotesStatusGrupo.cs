using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLotesStatusGrupo
{
    public int Codigo { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbLotesStatus> TbLotesStatuses { get; set; } = new List<TbLotesStatus>();
}

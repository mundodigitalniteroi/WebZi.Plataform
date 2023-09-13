using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvVistoriaSituacaoChassi
{
    public byte IdGrvVistoriaSituacaoChassi { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepGrvVistorium> TbDepGrvVistoria { get; set; } = new List<TbDepGrvVistorium>();
}

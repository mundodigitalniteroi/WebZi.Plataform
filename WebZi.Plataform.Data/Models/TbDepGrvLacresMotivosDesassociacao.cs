using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvLacresMotivosDesassociacao
{
    public byte IdLacreMotivoDesassociacao { get; set; }

    public string Descricao { get; set; }

    public string FlagExigirNovoLacre { get; set; }

    public virtual ICollection<TbDepGrvLacre> TbDepGrvLacres { get; set; } = new List<TbDepGrvLacre>();
}

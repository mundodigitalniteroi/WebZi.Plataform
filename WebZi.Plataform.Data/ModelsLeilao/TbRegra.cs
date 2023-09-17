using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbRegra
{
    public int Id { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbComitentesRegra> TbComitentesRegras { get; set; } = new List<TbComitentesRegra>();
}

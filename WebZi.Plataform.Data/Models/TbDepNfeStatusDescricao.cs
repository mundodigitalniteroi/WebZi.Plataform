using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepNfeStatusDescricao
{
    public string Status { get; set; }

    public string Descricao { get; set; }

    public string Explicacao { get; set; }

    public virtual ICollection<TbDepNfe> TbDepNves { get; set; } = new List<TbDepNfe>();
}

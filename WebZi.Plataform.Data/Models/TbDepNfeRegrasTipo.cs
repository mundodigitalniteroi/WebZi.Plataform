using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepNfeRegrasTipo
{
    public short NfeRegraTipoId { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public bool? PossuiValor { get; set; }

    public bool? Ativo { get; set; }

    public virtual ICollection<TbDepNfeRegra> TbDepNfeRegras { get; set; } = new List<TbDepNfeRegra>();
}

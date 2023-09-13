using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepClienteRegrasTipo
{
    public short ClienteRegraTipoId { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public string PossuiValor { get; set; }

    public string Ativo { get; set; }

    public virtual ICollection<TbDepClienteRegra> TbDepClienteRegras { get; set; } = new List<TbDepClienteRegra>();
}

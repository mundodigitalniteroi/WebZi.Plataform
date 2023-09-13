using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoRegrasTipo
{
    public short IdFaturamentoRegraTipo { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public string FlagPossuiValor { get; set; }

    public string FlagAtivo { get; set; }

    public virtual ICollection<TbDepFaturamentoRegra> TbDepFaturamentoRegras { get; set; } = new List<TbDepFaturamentoRegra>();
}

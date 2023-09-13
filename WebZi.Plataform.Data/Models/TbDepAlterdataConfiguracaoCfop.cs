using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataConfiguracaoCfop
{
    public short CfopId { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepAlterdataOperacao> TbDepAlterdataOperacaos { get; set; } = new List<TbDepAlterdataOperacao>();
}

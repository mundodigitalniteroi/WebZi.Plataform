using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoComposicaoNf
{
    public int IdFaturamentoComposicaoNf { get; set; }

    public int IdFaturamentoComposicao { get; set; }

    public string Nota { get; set; }

    public DateTime? DataEmissaoNota { get; set; }

    public virtual TbDepFaturamentoComposicao IdFaturamentoComposicaoNavigation { get; set; }
}

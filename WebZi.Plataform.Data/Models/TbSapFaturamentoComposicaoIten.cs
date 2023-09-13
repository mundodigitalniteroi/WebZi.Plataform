using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbSapFaturamentoComposicaoIten
{
    public int IdSapFaturamentoComposicaoItem { get; set; }

    public int IdSapFaturamentoComposicao { get; set; }

    public int IdFaturamentoComposicao { get; set; }
}

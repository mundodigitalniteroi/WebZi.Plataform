using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoCondicaoPagamento
{
    public byte IdFaturamentoCondicaoPagamento { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepFaturamentoTipoComposicao> TbDepFaturamentoTipoComposicaos { get; set; } = new List<TbDepFaturamentoTipoComposicao>();
}

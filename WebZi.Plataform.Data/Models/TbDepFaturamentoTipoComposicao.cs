using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoTipoComposicao
{
    public byte IdFaturamentoTipoComposicao { get; set; }

    public byte? IdFaturamentoCondicaoPagamento { get; set; }

    public string Origem { get; set; }

    public string Tipo { get; set; }

    public string CodigoSap { get; set; }

    public string Descricao { get; set; }

    public string DescricaoSap { get; set; }

    public virtual TbDepFaturamentoCondicaoPagamento IdFaturamentoCondicaoPagamentoNavigation { get; set; }

    public virtual ICollection<TbDepFaturamentoComposicao> TbDepFaturamentoComposicaos { get; set; } = new List<TbDepFaturamentoComposicao>();
}

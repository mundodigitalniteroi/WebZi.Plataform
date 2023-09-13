using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepTiposMeiosCobranca
{
    public byte IdTipoMeioCobranca { get; set; }

    public byte IdSapCondicaoPagamento { get; set; }

    public string CodigoSap { get; set; }

    public string Descricao { get; set; }

    public string Alias { get; set; }

    public string DocumentoImpressao { get; set; }

    public string FlagBanco { get; set; }

    public string FlagPossuiCodigoAutorizacaoCartao { get; set; }

    public string FlagAtivo { get; set; }

    public virtual ICollection<TbDepAlterdataConfiguracaoIdentificadorFormaPagamento> TbDepAlterdataConfiguracaoIdentificadorFormaPagamentos { get; set; } = new List<TbDepAlterdataConfiguracaoIdentificadorFormaPagamento>();

    public virtual ICollection<TbDepFaturamento> TbDepFaturamentos { get; set; } = new List<TbDepFaturamento>();
}

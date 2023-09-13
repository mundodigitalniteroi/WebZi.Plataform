using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataConfiguracaoIdentificadorFormaPagamento
{
    public short IdentificadorFormaPagamentoId { get; set; }

    public byte TipoMeioCobrancaId { get; set; }

    public string Codigo { get; set; }

    public virtual TbDepTiposMeiosCobranca TipoMeioCobranca { get; set; }
}

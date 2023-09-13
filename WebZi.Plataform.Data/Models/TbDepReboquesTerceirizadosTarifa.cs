using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepReboquesTerceirizadosTarifa
{
    public int IdReboqueTerceirizadoTarifas { get; set; }

    public int? IdReboqueTerceirizado { get; set; }

    public byte? IdTipoVeiculoClassificacaoNome { get; set; }

    public decimal? ValorTarifa { get; set; }

    public virtual TbDepReboquesTerceirizado IdReboqueTerceirizadoNavigation { get; set; }

    public virtual TbDepTipoVeiculosClassificacaoNome IdTipoVeiculoClassificacaoNomeNavigation { get; set; }
}

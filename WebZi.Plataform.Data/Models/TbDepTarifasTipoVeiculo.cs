using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepTarifasTipoVeiculo
{
    public int IdTarifaTipoVeiculo { get; set; }

    public int IdTarifa { get; set; }

    public byte IdTipoVeiculo { get; set; }

    public virtual TbDepTarifa IdTarifaNavigation { get; set; }

    public virtual TbDepTipoVeiculo IdTipoVeiculoNavigation { get; set; }
}

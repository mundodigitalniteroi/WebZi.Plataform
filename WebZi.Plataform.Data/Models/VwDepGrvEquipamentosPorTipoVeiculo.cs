using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepGrvEquipamentosPorTipoVeiculo
{
    public byte IdTipoVeiculo { get; set; }

    public string TipoVeiculoDescricao { get; set; }

    public decimal IdEquipamentoOpcional { get; set; }

    public string EquipamentoOpcionalDescricao { get; set; }

    public string EquipamentoItemObrigatorio { get; set; }

    public int? EquipamentoOrdemVistoria { get; set; }
}

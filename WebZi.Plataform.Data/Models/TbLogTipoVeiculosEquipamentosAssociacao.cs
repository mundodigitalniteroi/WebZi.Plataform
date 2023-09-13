using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogTipoVeiculosEquipamentosAssociacao
{
    public long Id { get; set; }

    public int IdTipoVeiculoEquipamentoAssociacao { get; set; }

    public byte IdTipoVeiculo { get; set; }

    public decimal IdEquipamentoOpcional { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int IdUsuarioExclusao { get; set; }

    public DateTime DataExclusao { get; set; }
}

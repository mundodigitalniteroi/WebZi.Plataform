using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepTipoVeiculosEquipamentosAssociacao
{
    public int IdTipoVeiculoEquipamentoAssociacao { get; set; }

    public byte IdTipoVeiculo { get; set; }

    public decimal IdEquipamentoOpcional { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepEquipamentosOpcionai IdEquipamentoOpcionalNavigation { get; set; }

    public virtual TbDepTipoVeiculo IdTipoVeiculoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }
}

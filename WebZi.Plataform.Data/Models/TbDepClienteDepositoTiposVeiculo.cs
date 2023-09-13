using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepClienteDepositoTiposVeiculo
{
    public short IdClienteDepositoTipoVeiculo { get; set; }

    public int IdClienteDeposito { get; set; }

    public byte IdTipoVeiculo { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }

    public virtual TbDepClientesDeposito IdClienteDepositoNavigation { get; set; }

    public virtual TbDepTipoVeiculo IdTipoVeiculoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }
}

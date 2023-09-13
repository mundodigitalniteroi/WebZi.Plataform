using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepTarifa
{
    public int IdTarifa { get; set; }

    public int IdClienteDeposito { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Descricao { get; set; }

    public decimal PrecoDiaria { get; set; }

    public decimal PrecoRebocada { get; set; }

    public decimal PrecoQuilometragem { get; set; }

    public DateTime DataVigenciaInicial { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public virtual TbDepClientesDeposito IdClienteDepositoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepTarifasTipoVeiculo> TbDepTarifasTipoVeiculos { get; set; } = new List<TbDepTarifasTipoVeiculo>();
}

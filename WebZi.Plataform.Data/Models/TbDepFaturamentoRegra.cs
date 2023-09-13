using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoRegra
{
    public short IdFaturamentoRegra { get; set; }

    public short IdFaturamentoRegraTipo { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDeposito { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Valor { get; set; }

    public DateTime DataVigenciaInicial { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public virtual TbDepCliente IdClienteNavigation { get; set; }

    public virtual TbDepDeposito IdDepositoNavigation { get; set; }

    public virtual TbDepFaturamentoRegrasTipo IdFaturamentoRegraTipoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepFaturamentoServicosAssociado> TbDepFaturamentoServicosAssociados { get; set; } = new List<TbDepFaturamentoServicosAssociado>();
}

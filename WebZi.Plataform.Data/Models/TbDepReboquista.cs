using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepReboquista
{
    public int IdReboquista { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Nome { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }

    public virtual TbDepCliente IdClienteNavigation { get; set; }

    public virtual TbDepDeposito IdDepositoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepGrv> TbDepGrvs { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepGtv> TbDepGtvs { get; set; } = new List<TbDepGtv>();

    public virtual ICollection<TbDepSolicitacaoReboque> TbDepSolicitacaoReboques { get; set; } = new List<TbDepSolicitacaoReboque>();
}

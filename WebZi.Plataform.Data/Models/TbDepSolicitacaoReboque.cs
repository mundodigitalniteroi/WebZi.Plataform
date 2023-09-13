using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSolicitacaoReboque
{
    public int IdSolicitacaoReboque { get; set; }

    public int IdClienteDeposito { get; set; }

    public int? IdReboque { get; set; }

    public int? IdReboquista { get; set; }

    public byte IdSolicitacaoReboqueTipo { get; set; }

    public byte IdSolicitacaoReboqueStatus { get; set; }

    public int? IdGrv { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string LocalRemocaoEnderecoCompleto { get; set; }

    public string LocalRemocaoReferencia { get; set; }

    public string LocalRemocaoLatitude { get; set; }

    public string LocalRemocaoLongitude { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public virtual TbDepClientesDeposito IdClienteDepositoNavigation { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepReboque IdReboqueNavigation { get; set; }

    public virtual TbDepReboquista IdReboquistaNavigation { get; set; }

    public virtual TbDepSolicitacaoReboqueStatus IdSolicitacaoReboqueStatusNavigation { get; set; }

    public virtual TbDepSolicitacaoReboqueTipo IdSolicitacaoReboqueTipoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepSolicitacaoReboqueDrfa> TbDepSolicitacaoReboqueDrfas { get; set; } = new List<TbDepSolicitacaoReboqueDrfa>();

    public virtual ICollection<TbDepSolicitacaoReboqueGrv> TbDepSolicitacaoReboqueGrvs { get; set; } = new List<TbDepSolicitacaoReboqueGrv>();

    public virtual ICollection<TbDepSolicitacaoReboquePsv> TbDepSolicitacaoReboquePsvs { get; set; } = new List<TbDepSolicitacaoReboquePsv>();
}

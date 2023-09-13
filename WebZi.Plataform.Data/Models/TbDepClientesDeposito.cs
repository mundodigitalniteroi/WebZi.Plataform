using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepClientesDeposito
{
    public int IdClienteDeposito { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public short? IdOrgaoEmissor { get; set; }

    public int IdEmpresa { get; set; }

    public string IdSistemaExterno { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string CodigoDetran { get; set; }

    public string CodigoSap { get; set; }

    public string CodigoSapOrdemVendas { get; set; }

    public string FlagUtilizaSistemaMobileGgv { get; set; }

    public string FlagCadastrarGrvBloqueado { get; set; }

    public string FlagValorIssIgualProdutoBaseCalculoAliquota { get; set; }

    public string FlagAtivo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public decimal AliquotaIss { get; set; }

    public virtual TbDepCliente IdClienteNavigation { get; set; }

    public virtual TbDepDeposito IdDepositoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual TbDepAlterdataConfiguracao TbDepAlterdataConfiguracao { get; set; }

    public virtual ICollection<TbDepClienteDepositoTiposVeiculo> TbDepClienteDepositoTiposVeiculos { get; set; } = new List<TbDepClienteDepositoTiposVeiculo>();

    public virtual ICollection<TbDepComunicacaoEmail> TbDepComunicacaoEmails { get; set; } = new List<TbDepComunicacaoEmail>();

    public virtual ICollection<TbDepContasTemporaria> TbDepContasTemporaria { get; set; } = new List<TbDepContasTemporaria>();

    public virtual ICollection<TbDepDetranAssociacaoTransacaoClienteDeposito> TbDepDetranAssociacaoTransacaoClienteDepositos { get; set; } = new List<TbDepDetranAssociacaoTransacaoClienteDeposito>();

    public virtual ICollection<TbDepNfeConfiguracaoImagem> TbDepNfeConfiguracaoImagems { get; set; } = new List<TbDepNfeConfiguracaoImagem>();

    public virtual ICollection<TbDepNfeRegra> TbDepNfeRegras { get; set; } = new List<TbDepNfeRegra>();

    public virtual ICollection<TbDepSolicitacaoReboque> TbDepSolicitacaoReboques { get; set; } = new List<TbDepSolicitacaoReboque>();

    public virtual ICollection<TbDepTarifa> TbDepTarifas { get; set; } = new List<TbDepTarifa>();
}

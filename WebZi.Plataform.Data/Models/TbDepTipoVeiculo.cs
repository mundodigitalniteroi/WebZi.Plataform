namespace WebZi.Plataform.Data.Models;

public partial class TbDepTipoVeiculo
{
    public byte IdTipoVeiculo { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Descricao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagNaoRequerCnhNaLiberacao { get; set; }

    public string FlagAtivo { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepClienteDepositoTiposVeiculo> TbDepClienteDepositoTiposVeiculos { get; set; } = new List<TbDepClienteDepositoTiposVeiculo>();

    public virtual ICollection<TbDepFaturamentoServicosTipoVeiculo> TbDepFaturamentoServicosTipoVeiculos { get; set; } = new List<TbDepFaturamentoServicosTipoVeiculo>();

    public virtual ICollection<TbDepGrv> TbDepGrvs { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepSolicitacaoReboquePsv> TbDepSolicitacaoReboquePsvs { get; set; } = new List<TbDepSolicitacaoReboquePsv>();

    public virtual ICollection<TbDepTarifasTipoVeiculo> TbDepTarifasTipoVeiculos { get; set; } = new List<TbDepTarifasTipoVeiculo>();

    public virtual ICollection<TbDepTipoVeiculosClassificacao> TbDepTipoVeiculosClassificacaos { get; set; } = new List<TbDepTipoVeiculosClassificacao>();

    public virtual ICollection<TbDepTipoVeiculosEquipamentosAssociacao> TbDepTipoVeiculosEquipamentosAssociacaos { get; set; } = new List<TbDepTipoVeiculosEquipamentosAssociacao>();
}

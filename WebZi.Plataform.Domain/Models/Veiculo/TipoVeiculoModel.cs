using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Veiculo
{
    public class TipoVeiculoModel
    {
        public byte TipoVeiculoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Descricao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public char FlagNaoRequerCnhNaLiberacao { get; set; }

        public char FlagAtivo { get; set; }

        public virtual Usuario.UsuarioModel UsuarioCadastro { get; set; }

        public virtual Usuario.UsuarioModel UsuarioAlteracao { get; set; }

        //public virtual ICollection<TbDepClienteDepositoTiposVeiculo> TbDepClienteDepositoTiposVeiculos { get; set; } = new List<TbDepClienteDepositoTiposVeiculo>();

        //public virtual ICollection<TbDepFaturamentoServicosTipoVeiculo> TbDepFaturamentoServicosTipoVeiculos { get; set; } = new List<TbDepFaturamentoServicosTipoVeiculo>();

        public virtual ICollection<GrvModel> Grvs { get; set; }

        //public virtual ICollection<TbDepSolicitacaoReboquePsv> TbDepSolicitacaoReboquePsvs { get; set; } = new List<TbDepSolicitacaoReboquePsv>();

        //public virtual ICollection<TbDepTarifasTipoVeiculo> TbDepTarifasTipoVeiculos { get; set; } = new List<TbDepTarifasTipoVeiculo>();

        //public virtual ICollection<TbDepTipoVeiculosClassificacao> TbDepTipoVeiculosClassificacaos { get; set; } = new List<TbDepTipoVeiculosClassificacao>();

        //public virtual ICollection<TbDepTipoVeiculosEquipamentosAssociacao> TbDepTipoVeiculosEquipamentosAssociacaos { get; set; } = new List<TbDepTipoVeiculosEquipamentosAssociacao>();
    }
}
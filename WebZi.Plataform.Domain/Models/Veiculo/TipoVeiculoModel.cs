using WebZi.Plataform.Domain.Models.Faturamento;
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

        public string FlagNaoRequerCnhNaLiberacao { get; set; }

        public string FlagAtivo { get; set; }

        public virtual Usuario.UsuarioModel UsuarioCadastro { get; set; }

        public virtual Usuario.UsuarioModel UsuarioAlteracao { get; set; }

        //public virtual ICollection<ClienteDepositoTiposVeiculo> ClienteDepositoTiposVeiculos { get; set; } = new List<ClienteDepositoTiposVeiculo>();

        public virtual ICollection<FaturamentoServicoTipoVeiculoModel> FaturamentoServicosTiposVeiculos { get; set; }

        public virtual ICollection<GrvModel> Grvs { get; set; }

        //public virtual ICollection<SolicitacaoReboquePsv> SolicitacaoReboquePsvs { get; set; }

        //public virtual ICollection<TarifasTipoVeiculo> TarifasTipoVeiculos { get; set; }

        //public virtual ICollection<TipoVeiculosClassificacao> TipoVeiculosClassificacaos { get; set; }

        // TODO: Implementar o Modelo
        //public virtual ICollection<TipoVeiculosEquipamentosAssociacao> TipoVeiculosEquipamentosAssociacaos { get; set; }
    }
}
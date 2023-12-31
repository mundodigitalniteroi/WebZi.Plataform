using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Usuario;

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

        public string FlagNaoRequerCnhNaLiberacao { get; set; } = "N";

        public string FlagAtivo { get; set; } = "S";

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        public virtual ICollection<ClienteDepositoTipoVeiculoModel> ClienteDepositoTiposVeiculos { get; set; }

        public virtual ICollection<FaturamentoServicoTipoVeiculoModel> FaturamentoServicosTiposVeiculos { get; set; }

        public virtual ICollection<TipoVeiculoClassificacaoModel> TiposVeiculosClassificacoes { get; set; }

        public virtual ICollection<TipoVeiculoEquipamentoAssociacaoModel> TiposVeiculosEquipamentosAssociacoes { get; set; }

        //public virtual ICollection<SolicitacaoReboquePsv> SolicitacaoReboquePsvs { get; set; }
    }
}
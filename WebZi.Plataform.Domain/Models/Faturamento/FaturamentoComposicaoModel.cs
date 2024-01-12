using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoComposicaoModel
    {
        public int FaturamentoComposicaoId { get; set; }

        public int FaturamentoId { get; set; }

        public int? FaturamentoServicoTipoVeiculoId { get; set; }

        public int? UsuarioDescontoId { get; set; }

        public int? UsuarioAlteracaoQuantidadeId { get; set; }

        public decimal ValorTipoComposicao { get; set; }

        public decimal? QuantidadeComposicao { get; set; } = 1;

        public decimal ValorComposicao { get; set; }

        /// <summary>
        /// Tipo da Cobrança:
        /// D = Diárias;
        /// H = Quantidade de HH:MM vezes o Preço;
        /// P = Porcentagem;
        /// Q = Quantidade;
        /// T = Tempo entre duas Datas;
        /// V = Valor.
        /// 
        /// Tabela de Descrição: tb_dep_tabela_generica, coluna ValorCadastro
        /// </summary>
        public string TipoComposicao { get; set; }

        /// <summary>
        /// Tipo do Desconto:
        /// P = Porcentagem;
        /// V = Valor.
        /// </summary>
        public string TipoDesconto { get; set; }

        /// <summary>
        /// Tipo do Lançamento:
        /// C = Crédito;
        /// D = Débito.
        /// </summary>
        public string TipoLancamento { get; set; }

        public int? QuantidadeDesconto { get; set; }

        public decimal? ValorDesconto { get; set; }

        public string ObservacaoDesconto { get; set; }

        public decimal ValorFaturado { get; set; }

        public decimal? QuantidadeAlterada { get; set; }

        public string ObservacaoQuantidadeAlterada { get; set; }

        public virtual FaturamentoModel Faturamento { get; set; }

        public virtual FaturamentoServicoTipoVeiculoModel FaturamentoServicoTipoVeiculo { get; set; }

        public virtual UsuarioModel UsuarioAlteracaoQuantidade { get; set; }

        public virtual UsuarioModel UsuarioDesconto { get; set; }

        //public virtual ICollection<FaturamentoComposicaoNf> FaturamentoComposicaoNfs { get; set; } = new List<FaturamentoComposicaoNf>();

        //public virtual ICollection<NfeFaturamentoComposicao> NfeFaturamentoComposicaos { get; set; } = new List<NfeFaturamentoComposicao>();
    }
}
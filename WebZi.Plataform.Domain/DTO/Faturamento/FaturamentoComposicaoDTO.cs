namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class FaturamentoComposicaoDTO
    {
        public int IdentificadorFaturamentoComposicao { get; set; }

        public int? IdentificadorFaturamentoServicoTipoVeiculo { get; set; }

        public int? IdentificadorUsuarioDesconto { get; set; }

        public int? IdentificadorUsuarioAlteracaoQuantidade { get; set; }

        /// <summary>
        /// Tipo da Cobrança:
        /// D = Diárias;
        /// H = Quantidade de HH:MM vezes o Preço;
        /// P = Porcentagem;
        /// Q = Quantidade;
        /// T = Tempo entre duas Datas;
        /// V = Valor.
        /// </summary>
        public string TipoComposicao { get; set; }

        public string DescricaoTipoComposicao { get; set; }

        public decimal? QuantidadeComposicao { get; set; } = 1;

        public decimal ValorTipoComposicao { get; set; }

        public decimal ValorComposicao { get; set; }

        public decimal ValorFaturado { get; set; }

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

        public decimal? QuantidadeAlterada { get; set; }

        public string ObservacaoQuantidadeAlterada { get; set; }
    }
}
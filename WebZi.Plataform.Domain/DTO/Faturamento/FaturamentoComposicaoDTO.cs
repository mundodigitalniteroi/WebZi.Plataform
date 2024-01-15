namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class FaturamentoComposicaoDTO
    {
        public int IdentificadorFaturamentoServico { get; set; }

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
        public string TipoServico { get; set; }

        public string DescricaoTipoServico { get; set; }

        public string NomeServico { get; set; }

        public DateTime DataVigenciaInicial { get; set; }

        public DateTime? DataVigenciaFinal { get; set; }

        public decimal? QuantidadeServico { get; set; } = 1;

        /// <summary>
        /// Tipo do Lançamento:
        /// C = Crédito;
        /// D = Débito.
        /// </summary>
        public string TipoLancamento { get; set; }

        public decimal ValorTipoServico { get; set; }

        public decimal ValorServico { get; set; }

        public decimal ValorFaturado { get; set; }

        /// <summary>
        /// Tipo do Desconto:
        /// P = Porcentagem;
        /// V = Valor.
        /// </summary>
        public string TipoDesconto { get; set; }

        public int? QuantidadeDesconto { get; set; }

        public decimal? ValorDesconto { get; set; }

        public string ObservacaoDesconto { get; set; }

        public decimal? QuantidadeAlterada { get; set; }

        public string ObservacaoQuantidadeAlterada { get; set; }
    }
}
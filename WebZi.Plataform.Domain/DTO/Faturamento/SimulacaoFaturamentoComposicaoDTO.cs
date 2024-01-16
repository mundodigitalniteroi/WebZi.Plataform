namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class SimulacaoFaturamentoComposicaoDTO
    {
        public int? IdentificadorFaturamentoServicoTipoVeiculo { get; set; }

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

        public decimal ValorFaturado { get; set; }
    }
}
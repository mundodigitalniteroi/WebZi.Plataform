namespace WebZi.Plataform.Domain.DTO.Faturamento.Cadastro
{
    public class FaturamentoCadastroComposicaoDTO
    {
        public int IdentificadorServico { get; set; }

        public int? IdentificadorFaturamentoServicoTipoVeiculo { get; set; }

        public string TipoServico { get; set; }

        public string DescricaoTipoServico { get; set; }

        public string NomeServico { get; set; }

        public DateTime DataVigenciaInicial { get; set; }

        public DateTime? DataVigenciaFinal { get; set; }

        public decimal? QuantidadeServico { get; set; }

        public string TipoLancamento { get; set; }

        public decimal ValorTipoServico { get; set; }

        public decimal ValorFaturado { get; set; }
    }
}
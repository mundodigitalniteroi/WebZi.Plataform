namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoFaturamentoDescontoModel
    {
        public int FaturamentoServicoTipoVeiculoId { get; set; }

        public char TipoComposicao { get; set; }

        public int FaturamentoTipoComposicaoId { get; set; }

        public int UsuarioDescontoId { get; set; }

        public string TipoDesconto { get; set; } // P = Porcentagem, V = Valor

        public int QuantidadeDesconto { get; set; }

        public decimal ValorDesconto { get; set; }

        public string ObservacaoDesconto { get; set; }
    }
}
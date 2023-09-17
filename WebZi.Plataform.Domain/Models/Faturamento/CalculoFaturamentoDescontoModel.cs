namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoFaturamentoDescontoModel
    {
        public int id_faturamento_servico_tipo_veiculo { get; set; }

        public char tipo_composicao { get; set; }

        public int id_faturamento_tipo_composicao { get; set; }

        public int id_usuario_desconto { get; set; }

        public char tipo_desconto { get; set; } // P = Porcentagem, V = Valor

        public int quantidade_desconto { get; set; }

        public decimal valor_desconto { get; set; }

        public string observacao_desconto { get; set; }
    }
}
namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoFaturamentoQuantidadeAlteradaModel
    {
        public int id_faturamento_tipo_composicao { get; set; }

        public int id_faturamento_servico_tipo_veiculo { get; set; }

        public char tipo_composicao { get; set; }

        public int id_usuario_alteracao_quantidade { get; set; }

        public int quantidade_alterada { get; set; }

        public string observacao_quantidade_alterada { get; set; }
    }
}
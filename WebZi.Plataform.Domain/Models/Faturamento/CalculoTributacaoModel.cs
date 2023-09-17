namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoTributacaoModel
    {
        public string codigo_material { get; set; }

        public int id_faturamento_servico_associado { get; set; }

        public string servico_descricao { get; set; }
    }
}
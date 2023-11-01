namespace WebZi.Plataform.Domain.Models.Sistema
{
    public class TabelaGenericaModel
    {
        public int TabelaGenericaId { get; set; }

        public string Codigo { get; set; }

        public string Sigla { get; set; }

        public string ValorCadastro { get; set; }

        public byte Sequencia { get; set; }

        public string Descricao { get; set; }
    }
}
namespace WebZi.Plataform.Domain.Models.Leilao
{
    public class LeilaoStatusModel
    {
        public int LeilaoStatusId { get; set; }

        public string Descricao { get; set; }

        public string ExibeMensagemConferencia { get; set; } = "N";

        public int? Sequencia { get; set; }

        public string Ativo { get; set; } = "A";
    }
}
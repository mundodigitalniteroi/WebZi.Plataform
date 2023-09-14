namespace WebZi.Plataform.Domain.Models.Atendimento
{
    public class AtendimentoAvisoViewModel
    {
        public string Status { get; set; }

        public List<string> Avisos { get; set; } = new List<string>();

        public List<string> Erros { get; set; } = new List<string>();
    }
}
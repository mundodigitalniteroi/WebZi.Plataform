namespace WebZi.Plataform.Domain.Models
{
    public class MensagemViewModel
    {
        public string Status { get; set; }

        public List<string> AvisosInformativos { get; set; } = new List<string>();

        public List<string> AvisosImpeditivos { get; set; } = new List<string>();

        public List<string> Erros { get; set; } = new List<string>();
    }
}
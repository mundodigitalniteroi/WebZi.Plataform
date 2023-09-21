namespace WebZi.Plataform.Domain.Models
{
    public class AvisoViewModel
    {
        public string Status { get; set; }

        public List<string> Avisos { get; set; } = new List<string>();

        public List<string> Erros { get; set; } = new List<string>();
    }
}
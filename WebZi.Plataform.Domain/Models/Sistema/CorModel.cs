namespace WebZi.Plataform.Domain.Models.Sistema
{
    public class CorModel
    {
        public int CorId { get; set; }

        public string Cor { get; set; }

        public string CorSecundaria { get; set; }

        public string FlagCorPrincipal { get; set; } = "N";

        public string FlagAtivo { get; set; } = "S";
    }
}
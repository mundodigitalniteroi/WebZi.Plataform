namespace WebZi.Plataform.Domain.Models.Localizacao
{
    public class RegiaoModel
    {
        public string RegiaoId { get; set; }

        public string Nome { get; set; }

        public virtual ICollection<EstadoModel> Estados { get; set; }
    }
}
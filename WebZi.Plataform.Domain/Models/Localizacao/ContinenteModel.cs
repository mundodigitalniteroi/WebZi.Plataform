namespace WebZi.Plataform.Domain.Models.Localizacao
{
    public class ContinenteModel
    {
        public string ContinenteId { get; set; }

        public string Nome { get; set; }

        public string NomePtbr { get; set; }

        public virtual ICollection<PaisModel> Paises { get; set; }
    }
}
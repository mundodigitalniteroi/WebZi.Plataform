namespace WebZi.Plataform.Domain.Models.Localizacao
{
    public class PaisModel
    {
        public string PaisNumcode { get; set; }

        public string ContinenteId { get; set; }

        public string Iso { get; set; }

        public string Iso3 { get; set; }

        public string Nome { get; set; }

        public string NomePtbr { get; set; }

        public virtual ContinenteModel Continente { get; set; }

        public virtual ICollection<EstadoModel> Estados { get; set; }
    }
}
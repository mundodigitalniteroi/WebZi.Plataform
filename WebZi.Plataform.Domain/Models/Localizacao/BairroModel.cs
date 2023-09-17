namespace WebZi.Plataform.Domain.Models.Localizacao
{
    public class BairroModel
    {
        public int BairroId { get; set; }

        public int MunicipioId { get; set; }

        public string Nome { get; set; }

        public string NomePtbr { get; set; }

        public virtual MunicipioModel Municipio { get; set; }

        public virtual CEPModel CEP { get; set; }
    }
}
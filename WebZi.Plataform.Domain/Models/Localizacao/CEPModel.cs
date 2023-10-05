namespace WebZi.Plataform.Domain.Models.Localizacao
{
    public class CEPModel
    {
        public int CEPId { get; set; }

        public int MunicipioId { get; set; }

        public int? BairroId { get; set; }

        public byte? TipoLogradouroId { get; set; }

        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string FlagSanitizado { get; set; } = "N";

        public virtual BairroModel Bairro { get; set; }

        public virtual MunicipioModel Municipio { get; set; }

        public virtual TipoLogradouroModel TipoLogradouro { get; set; }
    }
}
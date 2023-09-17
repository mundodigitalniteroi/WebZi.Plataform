namespace WebZi.Plataform.Domain.Models.Localizacao
{
    public class MunicipioModel
    {
        public int MunicipioId { get; set; }

        public string Uf { get; set; }

        public string Nome { get; set; }

        public string NomePtbr { get; set; }

        public string CodigoMunicipio { get; set; }

        public string CodigoMunicipioIbge { get; set; }

        public byte? EstadoId { get; set; }

        public virtual EstadoModel Estado { get; set; }

        public virtual CEPModel CEP { get; set; }

        public virtual ICollection<BairroModel> Bairros { get; set; }

        public virtual ICollection<FeriadoModel> Feriados { get; set; }
    }
}
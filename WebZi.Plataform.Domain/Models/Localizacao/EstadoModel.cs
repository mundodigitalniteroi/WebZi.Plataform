namespace WebZi.Plataform.Domain.Models.Localizacao
{
    public class EstadoModel
    {
        public byte EstadoId { get; set; }

        public string UF { get; set; }

        public string PaisNumcode { get; set; }

        public string RegiaoId { get; set; }

        public string Nome { get; set; }

        public string NomePtbr { get; set; }

        public string Capital { get; set; }

        public byte UtcId { get; set; }

        public byte? UtcVeraoId { get; set; }

        public virtual PaisModel Pais { get; set; }

        public virtual RegiaoModel Regiao { get; set; }

        public virtual ICollection<FeriadoModel> Feriados { get; set; }

        public virtual ICollection<MunicipioModel> Municipios { get; set; }

        // public virtual ICollection<OrgaoEmissorModel> OrgaosEmissores { get; set; }
    }
}
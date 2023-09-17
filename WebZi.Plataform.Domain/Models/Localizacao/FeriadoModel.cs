namespace WebZi.Plataform.Domain.Models.Localizacao
{
    public class FeriadoModel
    {
        public short FeriadoId { get; set; }

        public string Uf { get; set; }

        public int? MunicipioId { get; set; }

        public int Dia { get; set; }

        public int Mes { get; set; }

        public int? Ano { get; set; }

        public string Descricao { get; set; }

        public string FlagFeriadoEstadual { get; set; } = "N";

        public string FlagFeriadoNacional { get; set; } = "N";

        public virtual MunicipioModel Municipio { get; set; }

        public virtual EstadoModel Estado { get; set; }
    }
}
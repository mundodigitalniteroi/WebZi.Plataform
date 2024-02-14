namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia
{
    public class PixDinamicoConsultaModel
    {
        public int PixDinamicoConsultaId { get; set; }

        public int PixDinamicoId { get; set; }

        public byte PixDinamicoTipoStatusGeracaoId { get; set; }

        public string Json { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual PixDinamicoModel PixDinamico { get; set; }

        public virtual PixDinamicoTipoStatusGeracaoModel PixDinamicoTipoStatusGeracao { get; set; }
    }
}
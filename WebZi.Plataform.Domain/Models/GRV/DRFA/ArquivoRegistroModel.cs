namespace WebZi.Plataform.Domain.Models.GRV.DRFA
{
    public class ArquivoRegistroModel
    {
        public int GrvDRFAAqruivoRegistroId { get; set; }
        public int GrvDRFAId { get; set; }
        public string NomeArquivo  { get; set; }
        public byte[] ArquivoRegistro { get; set; }
        public char TipoArquivo { get; set; }

        public DRFAModel DRFA { get; set; }
    }
}

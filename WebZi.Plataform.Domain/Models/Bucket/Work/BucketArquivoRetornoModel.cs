namespace WebZi.Plataform.Domain.Models.Bucket.Work
{
    public class BucketArquivoRetornoModel : BucketErrorModel
    {
        public int Id { get; set; }

        public string NomeArquivo { get; set; }

        public int TamanhoBytes { get; set; }

        public string Url { get; set; }

        public string TipoArquivo { get; set; }

        public string PermissaoAcesso { get; set; }

        public string NomeArquivoCompleto { get; set; }

        public string NomeArquivoOriginal { get; set; }
    }
}
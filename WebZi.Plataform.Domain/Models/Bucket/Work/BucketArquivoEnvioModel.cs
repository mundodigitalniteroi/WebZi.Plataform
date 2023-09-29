namespace WebZi.Plataform.Domain.Models.Bucket.Work
{
    public class BucketArquivoEnvioModel : BucketErrorModel
    {
        public string NomeBucket { get; set; }

        public string NomeArquivo { get; set; }

        public string ArquivoBase64 { get; set; }

        public string TipoArquivo { get; set; } = "image/jpeg";

        public string NomePasta { get; set; }

        public string PermissaoAcesso { get; set; } = "PUBLICO";

        public bool Atualizar { get; set; } = true;

        public string NomeArquivoCompleto { get; set; }

        public string NomeArquivoOriginal { get; set; }

        public byte[] Imagem { get; set; }
    }
}
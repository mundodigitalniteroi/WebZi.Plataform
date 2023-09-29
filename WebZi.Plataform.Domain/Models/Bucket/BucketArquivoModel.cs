using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Bucket
{
    public class BucketArquivoModel
    {
        public int RepositorioArquivoId { get; set; }

        public short NomeTabelaOrigemId { get; set; }

        // Representa o ID da Tabela mãe que gerou a imagem/arquivo
        public int TabelaOrigemId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public string NomeArquivo { get; set; }

        public int TamanhoBytes { get; set; }

        public string Url { get; set; }

        public string TipoArquivo { get; set; } = "image/jpeg";

        public string PermissaoAcesso { get; set; } = "PUBLICO";

        public DateTime DataHoraCadastro { get; set; }

        /// <summary>
        /// Coluna usada apenas no cadastro das Fotos do GGV:
        /// E - EntradaPatio
        /// R - Regularizacao
        /// V - Vistoria
        /// </summary>
        public string TipoCadastro { get; set; }

        public virtual BucketNomeTabelaOrigemModel BucketNomeTabelaOrigem { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }
    }
}
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Leilao
{
    public class LiberacaoLeilaoModel
    {
        public int LiberacaoLeilaoId { get; set; }

        public int GrvId { get; set; }

        public string StatusOperacaoLeilaoId { get; set; } = "1";

        public int UsuarioCadastroId { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual GrvModel Grv { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }
    }
}
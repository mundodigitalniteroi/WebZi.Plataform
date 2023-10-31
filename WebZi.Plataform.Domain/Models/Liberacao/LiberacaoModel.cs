using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Liberacao
{
    public class LiberacaoModel
    {
        public int LiberacaoId { get; set; }

        public byte TipoLiberacaoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual TipoLiberacaoModel TipoLiberacao { get; set; }

        public virtual UsuarioModel Usuario { get; set; }

        public virtual GrvModel Grv { get; set; }
    }
}
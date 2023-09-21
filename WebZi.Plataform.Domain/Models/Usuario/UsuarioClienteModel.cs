using WebZi.Plataform.Domain.Models.Cliente;

namespace WebZi.Plataform.Domain.Models.Usuario
{
    public class UsuarioClienteModel
    {
        public long UsuarioClienteId { get; set; }

        public int UsuarioId { get; set; }

        public int ClienteId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual ClienteModel Cliente { get; set; }

        public virtual UsuarioModel Usuario { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }
    }
}
using WebZi.Plataform.Domain.Models.Deposito;

namespace WebZi.Plataform.Domain.Models.Usuario
{
    public class UsuarioDepositoModel
    {
        public long UsuarioDepositoId { get; set; }

        public int UsuarioId { get; set; }

        public int DepositoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual DepositoModel Deposito { get; set; }

        public virtual UsuarioModel Usuario { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }
    }
}
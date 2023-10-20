using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Views.Usuario
{
    public class ViewUsuarioClienteDepositoModel
    {
        public long UsuarioClienteId { get; set; }

        public long UsuarioDepositoId { get; set; }

        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public int UsuarioId { get; set; }

        public string Login { get; set; }

        public string Senha1 { get; set; }

        public string FlagAtivo { get; set; }

        public UsuarioModel Usuario { get; set; }
    }
}
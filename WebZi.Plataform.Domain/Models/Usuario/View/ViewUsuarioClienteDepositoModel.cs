namespace WebZi.Plataform.Domain.Models.Usuario.View
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

        public string UsuarioFlagAtivo { get; set; }
    }
}
namespace WebZi.Plataform.Domain.DTO.Usuario
{
    public class UsuarioClienteDepositoReboqueDTO
    {
        public int IdentificadorCliente { get; set; }

        public string ClienteNome { get; set; }

        public string ClienteFlagAtivo { get; set; }

        public int IdentificadorDeposito { get; set; }

        public string DepositoNome { get; set; }

        public string DepositoFlagAtivo { get; set; }

        public int IdentificadorUsuario { get; set; }

        public string UsuarioFlagAtivo { get; set; }

        public int IdentificadorReboque { get; set; }

        public string ReboquePlaca { get; set; }

        public string ReboqueFlagAtivo { get; set; }
    }
}
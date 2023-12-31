namespace WebZi.Plataform.Domain.DTO.Usuario
{
    public class UsuarioClienteDepositoReboquistaDTO
    {
        public int IdentificadorCliente { get; set; }

        public string ClienteNome { get; set; }

        public string ClienteFlagAtivo { get; set; }

        public int IdentificadorDeposito { get; set; }

        public string DepositoNome { get; set; }

        public string DepositoFlagAtivo { get; set; }

        public int IdentificadorUsuario { get; set; }

        public string UsuarioFlagAtivo { get; set; }

        public int IdentificadorReboquista { get; set; }

        public string ReboquistaNome { get; set; }

        public string ReboquistaFlagAtivo { get; set; }
    }
}
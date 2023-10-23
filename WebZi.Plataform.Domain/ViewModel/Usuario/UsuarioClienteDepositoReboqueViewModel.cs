namespace WebZi.Plataform.Domain.ViewModel.Usuario
{
    public class UsuarioClienteDepositoReboqueViewModel
    {
        public int ClienteId { get; set; }

        public string ClienteNome { get; set; }

        public string ClienteFlagAtivo { get; set; }

        public int DepositoId { get; set; }

        public string DepositoNome { get; set; }

        public string DepositoFlagAtivo { get; set; }

        public int UsuarioId { get; set; }

        public string UsuarioFlagAtivo { get; set; }

        public int ReboqueId { get; set; }

        public string ReboquePlaca { get; set; }

        public string ReboqueFlagAtivo { get; set; }
    }
}
namespace WebZi.Plataform.Domain.ViewModel.Usuario
{
    public class UsuarioViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public int IdentificadorUsuario { get; set; }

        public string Token { get; set; }

        public string FlagPermissaoDesconto { get; set; }

        public string FlagPermissaoDataRetroativaFaturamento { get; set; }

        public string FlagAtivo { get; set; }

        public List<UsuarioClienteDepositoViewModel> ListagemClienteDepositoAssociado { get; set; } = new();
    }
}
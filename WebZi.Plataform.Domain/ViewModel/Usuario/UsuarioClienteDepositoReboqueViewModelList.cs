namespace WebZi.Plataform.Domain.ViewModel.Usuario
{
    public class UsuarioClienteDepositoReboqueViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<UsuarioClienteDepositoReboqueViewModel> Listagem { get; set; } = new();
    }
}
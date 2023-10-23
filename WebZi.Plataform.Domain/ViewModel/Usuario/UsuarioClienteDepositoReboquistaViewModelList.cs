namespace WebZi.Plataform.Domain.ViewModel.Usuario
{
    public class UsuarioClienteDepositoReboquistaViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<UsuarioClienteDepositoReboquistaViewModel> ListagemUsuarioClienteDepositoReboquista { get; set; } = new();
    }
}
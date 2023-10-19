namespace WebZi.Plataform.Domain.ViewModel.Cliente
{
    public class ClienteViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<ClienteViewModel> ListagemCliente { get; set; } = new();
    }
}
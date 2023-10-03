namespace WebZi.Plataform.Domain.ViewModel.Cliente
{
    public class ClienteViewModelList
    {
        public List<ClienteViewModel> Clientes { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
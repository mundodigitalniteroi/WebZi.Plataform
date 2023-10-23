namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class ClienteSimplificadoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; }

        public List<ClienteSimplificadoViewModel> ListagemCliente { get; set; } = new();
    }
}
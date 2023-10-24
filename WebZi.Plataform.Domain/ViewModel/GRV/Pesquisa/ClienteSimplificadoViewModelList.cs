namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class ClienteSimplificadoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; }

        public List<ClienteSimplificadoViewModel> Listagem { get; set; } = new();
    }
}
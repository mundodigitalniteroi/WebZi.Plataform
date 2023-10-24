namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class ClienteDepositoSimplificadoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; }

        public List<ClienteDepositoSimplificadoViewModel> Listagem { get; set; } = new();
    }
}
namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class ClienteDepositoSimplificadoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; }

        public List<ClienteDepositoSimplificadoViewModel> ListagemDeposito { get; set; } = new();
    }
}
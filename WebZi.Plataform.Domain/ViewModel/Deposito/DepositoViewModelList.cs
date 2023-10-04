namespace WebZi.Plataform.Domain.ViewModel.Deposito
{
    public class DepositoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<DepositoViewModel> Depositos { get; set; } = new();
    }
}
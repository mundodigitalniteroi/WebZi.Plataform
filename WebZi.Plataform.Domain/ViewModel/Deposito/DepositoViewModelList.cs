namespace WebZi.Plataform.Domain.ViewModel.Deposito
{
    public class DepositoViewModelList
    {
        public List<DepositoViewModel> Depositos { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
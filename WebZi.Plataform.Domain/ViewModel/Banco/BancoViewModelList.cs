namespace WebZi.Plataform.Domain.ViewModel.Banco
{
    public class BancoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();
        
        public List<BancoViewModel> ListagemBanco { get; set; } = new();
    }
}
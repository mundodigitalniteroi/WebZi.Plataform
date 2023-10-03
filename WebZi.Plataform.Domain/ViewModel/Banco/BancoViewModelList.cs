namespace WebZi.Plataform.Domain.ViewModel.Banco
{
    public class BancoViewModelList
    {
        public List<BancoViewModel> Bancos { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
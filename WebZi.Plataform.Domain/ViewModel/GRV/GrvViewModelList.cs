namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class GrvViewModelList
    {
        public List<GrvViewModel> Grvs { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
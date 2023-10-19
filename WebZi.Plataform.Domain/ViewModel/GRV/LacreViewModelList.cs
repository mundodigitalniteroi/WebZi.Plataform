namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class LacreViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<LacreViewModel> ListagemLacre { get; set; } = new();
    }
}
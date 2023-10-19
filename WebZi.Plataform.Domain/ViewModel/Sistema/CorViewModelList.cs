namespace WebZi.Plataform.Domain.ViewModel.Sistema
{
    public class CorViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<CorViewModel> ListagemCor { get; set; } = new();
    }
}
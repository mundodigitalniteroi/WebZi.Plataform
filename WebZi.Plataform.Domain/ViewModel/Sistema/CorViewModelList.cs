namespace WebZi.Plataform.Domain.ViewModel.Sistema
{
    public class CorViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<CorViewModel> Listagem { get; set; } = new();
    }
}
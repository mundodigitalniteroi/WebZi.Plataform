namespace WebZi.Plataform.Domain.ViewModel.Servico
{
    public class ReboqueViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<ReboqueViewModel> Listagem { get; set; } = new();
    }
}
namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class StatusAssinaturaCondutorViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<StatusAssinaturaCondutorViewModel> Listagem { get; set; } = new();
    }
}
namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class StatusAssinaturaCondutorViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<StatusAssinaturaCondutorViewModel> ListagemStatusAssinaturaCondutor { get; set; } = new();
    }
}
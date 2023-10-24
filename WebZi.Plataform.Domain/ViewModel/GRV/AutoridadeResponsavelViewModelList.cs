namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class AutoridadeResponsavelViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<AutoridadeResponsavelViewModel> Listagem { get; set; } = new();
    }
}
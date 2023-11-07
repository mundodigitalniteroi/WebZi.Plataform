namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class ServicoAssociadoGrvViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new MensagemViewModel();

        public List<ServicoAssociadoGrvViewModel> Listagem { get; set; } = new();
    }
}
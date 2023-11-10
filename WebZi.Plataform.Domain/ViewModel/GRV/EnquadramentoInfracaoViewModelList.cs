namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class EnquadramentoInfracaoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<EnquadramentoInfracaoViewModel> Listagem { get; set; } = new();
    }
}
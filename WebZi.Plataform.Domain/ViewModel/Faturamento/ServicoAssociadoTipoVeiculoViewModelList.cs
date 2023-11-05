namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class ServicoAssociadoTipoVeiculoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new MensagemViewModel();

        public List<ServicoAssociadoTipoVeiculoViewModel> Listagem { get; set; } = new();
    }
}
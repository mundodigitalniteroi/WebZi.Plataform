namespace WebZi.Plataform.Domain.ViewModel.Veiculo
{
    public class TipoVeiculoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<TipoVeiculoViewModel> TiposVeiculos { get; set; } = new();
    }
}
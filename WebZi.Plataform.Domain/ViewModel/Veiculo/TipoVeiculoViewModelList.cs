namespace WebZi.Plataform.Domain.ViewModel.Veiculo
{
    public class TipoVeiculoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<TipoVeiculoViewModel> Listagem { get; set; } = new();
    }
}
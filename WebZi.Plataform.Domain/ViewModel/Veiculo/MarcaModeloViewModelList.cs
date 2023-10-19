namespace WebZi.Plataform.Domain.ViewModel.Veiculo
{
    public class MarcaModeloViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<MarcaModeloViewModel> ListagemMarcaModelo { get; set; } = new();
    }
}
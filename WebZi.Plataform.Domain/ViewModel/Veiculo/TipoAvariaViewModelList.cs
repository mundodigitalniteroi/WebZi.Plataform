namespace WebZi.Plataform.Domain.ViewModel.Veiculo
{
    public class TipoAvariaViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<TipoAvariaViewModel> ListagemTipoAvaria { get; set; } = new();
    }
}
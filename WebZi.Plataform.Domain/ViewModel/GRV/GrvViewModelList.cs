namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class GrvViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<GrvViewModel> ListagemGrv { get; set; } = new();
    }
}
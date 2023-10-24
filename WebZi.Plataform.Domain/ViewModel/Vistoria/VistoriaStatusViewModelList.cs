namespace WebZi.Plataform.Domain.ViewModel.Vistoria
{
    public class VistoriaStatusViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<VistoriaStatusViewModel> Listagem { get; set; } = new();
    }
}
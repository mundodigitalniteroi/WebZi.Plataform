namespace WebZi.Plataform.Domain.ViewModel.Servico
{
    public class ReboquistaViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<ReboquistaViewModel> Reboquistas { get; set; } = new();
    }
}
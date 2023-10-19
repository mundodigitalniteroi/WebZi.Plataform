namespace WebZi.Plataform.Domain.ViewModel.Servico
{
    public class ReboquistaViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<ReboquistaViewModel> ListagemReboquista { get; set; } = new();
    }
}
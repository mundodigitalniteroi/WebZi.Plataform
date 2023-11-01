namespace WebZi.Plataform.Domain.ViewModel.Generic
{
    public class TabelaGenericaViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new MensagemViewModel();

        public List<TabelaGenericaViewModel> Listagem { get; set; } = new();
    }
}
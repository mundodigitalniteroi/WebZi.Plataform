namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class ReboquistaSimplificadoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; }

        public List<ReboquistaSimplificadoViewModel> Listagem { get; set; } = new();
    }
}
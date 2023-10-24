namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class ReboqueSimplificadoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; }

        public List<ReboqueSimplificadoViewModel> Listagem { get; set; } = new();
    }
}
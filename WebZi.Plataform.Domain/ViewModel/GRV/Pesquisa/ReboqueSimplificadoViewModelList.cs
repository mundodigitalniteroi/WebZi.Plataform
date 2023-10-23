namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class ReboqueSimplificadoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; }

        public List<ReboqueSimplificadoViewModel> ListagemReboque { get; set; } = new();
    }
}
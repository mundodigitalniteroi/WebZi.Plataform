namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class GrvPesquisaResultViewModelList
    {
        public MensagemViewModel Mensagem { get; set; }

        public List<GrvPesquisaResultViewModel> Grvs { get; set; } = new();
    }
}
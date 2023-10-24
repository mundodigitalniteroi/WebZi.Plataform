namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class TipoMeioCobrancaViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<TipoMeioCobrancaViewModel> Listagem { get; set; } = new();
    }
}
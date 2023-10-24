namespace WebZi.Plataform.Domain.ViewModel.Banco
{
    public class AgenciaBancariaViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<AgenciaBancariaViewModel> Listagem { get; set; } = new();
    }
}
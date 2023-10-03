namespace WebZi.Plataform.Domain.ViewModel.Banco
{
    public class AgenciaBancariaViewModelList
    {
        public List<AgenciaBancariaViewModel> AgenciasBancarias { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
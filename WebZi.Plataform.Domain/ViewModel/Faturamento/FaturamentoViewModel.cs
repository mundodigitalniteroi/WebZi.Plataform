namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class FaturamentoViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public int IdentificadorFaturamento { get; set; }
    }
}
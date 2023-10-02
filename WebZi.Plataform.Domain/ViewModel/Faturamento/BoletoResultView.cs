using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class BoletoResultView
    {
        public byte[] Boleto { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new MensagemViewModel();
    }
}
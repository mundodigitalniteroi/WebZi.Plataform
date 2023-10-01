using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.Domain.Models.Faturamento.ViewModel
{
    public class BoletoResultView
    {
        public byte[] Boleto { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new MensagemViewModel();
    }
}
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class TipoMeioCobrancaViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<TipoMeioCobrancaModel> TiposMeiosCobrancas { get; set; } = new();
    }
}
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class StatusOperacaoViewModelList
    {
        public List<StatusOperacaoModel> StatusOperacoes { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
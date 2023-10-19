using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class StatusOperacaoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<StatusOperacaoModel> ListagemStatusOperacao { get; set; } = new();
    }
}
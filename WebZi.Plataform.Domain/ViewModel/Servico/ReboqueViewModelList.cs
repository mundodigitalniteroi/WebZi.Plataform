using WebZi.Plataform.Domain.ViewModel.GRV;

namespace WebZi.Plataform.Domain.ViewModel.Servico
{
    public class ReboqueViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<ReboqueViewModel> ListagemReboque { get; set; } = new();
    }
}
using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class AtendimentoCadastroResultViewModel
    {
        public int AtendimentoId { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
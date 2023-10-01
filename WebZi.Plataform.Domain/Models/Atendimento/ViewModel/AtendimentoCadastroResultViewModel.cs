using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.Domain.Models.Atendimento.ViewModel
{
    public class AtendimentoCadastroResultViewModel
    {
        public int AtendimentoId { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
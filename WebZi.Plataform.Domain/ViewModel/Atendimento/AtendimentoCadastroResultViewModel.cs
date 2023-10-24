namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class AtendimentoCadastroResultViewModel
    {
        public int IdentificadorAtendimento { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class AtendimentoFotoResponsavelViewModel
    {
        public byte[] Foto { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Atendimento
{
    public class AtendimentoCadastroDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorAtendimento { get; set; }
    }
}
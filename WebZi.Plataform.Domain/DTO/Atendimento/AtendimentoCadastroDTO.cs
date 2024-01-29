using WebZi.Plataform.Domain.DTO.Faturamento.Cadastro;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Atendimento
{
    public class AtendimentoCadastroDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorAtendimento { get; set; }

        public FaturamentoCadastroDTO Faturamento { get; set; }
    }
}
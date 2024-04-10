using WebZi.Plataform.Domain.DTO.Faturamento.Cadastro;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class FaturamentoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorProcesso { get; set; } = new();

        public int IdentificadorAtendimento { get; set; } = new();

        public FaturamentoCadastroDTO Faturamento { get; set; }
    }
}
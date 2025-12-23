using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco.PIX
{
    public class SenhaPixEstaticoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorFaturamento { get; set; }

        public string Senha { get; set; }

    }
}
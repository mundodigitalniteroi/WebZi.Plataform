using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco.PIX
{
    public class SenhaValidaDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public bool EValida { get; set; }

    }
}
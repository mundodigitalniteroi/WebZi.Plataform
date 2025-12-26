using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco.PIX
{
    public class SenhaValidandoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public bool EValida { get; set; }

    }
}
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco
{
    public class BoletoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new MensagemDTO();

        public int Identificador { get; set; }

        public string LinhaDigitavel { get; set; }

        public byte[] Imagem { get; set; }
    }
}
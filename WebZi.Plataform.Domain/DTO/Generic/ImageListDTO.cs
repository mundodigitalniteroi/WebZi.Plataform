using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Generic
{
    public class ImageListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new MensagemDTO();

        public List<ImageDTO> Listagem { get; set; } = new();
    }
}
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.DRFA
{
    public class ListArquivosDRFADTO
    {
        public MensagemDTO Mensagem { get; set; } = new();
        public ImageDTO? ArquivoRegistroFurtoRoubo { get; set; }
        public ImageDTO? ArquivoDeRecuperacao { get; set; }
    }
}

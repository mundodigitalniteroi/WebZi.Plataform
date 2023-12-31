using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Generic
{
    public class TabelaGenericaListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new MensagemDTO();

        public List<TabelaGenericaDTO> Listagem { get; set; } = new();
    }
}
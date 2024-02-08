using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco
{
    public class BoletoOriginalListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new MensagemDTO();

        public List<BoletoOriginalDTO> Listagem { get; set; } = new();
    }
}
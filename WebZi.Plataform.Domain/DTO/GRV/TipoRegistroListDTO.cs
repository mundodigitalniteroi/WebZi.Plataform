using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class TipoRegistroListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public IEnumerable<TipoRegistroDTO> Listagem { get; set; }
    }
}

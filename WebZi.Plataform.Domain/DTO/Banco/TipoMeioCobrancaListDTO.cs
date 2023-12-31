using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco
{
    public class TipoMeioCobrancaListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<TipoMeioCobrancaDTO> Listagem { get; set; } = new();
    }
}
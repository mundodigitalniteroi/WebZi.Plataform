using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Veiculo
{
    public class TipoAvariaListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<TipoAvariaDTO> Listagem { get; set; } = new();
    }
}
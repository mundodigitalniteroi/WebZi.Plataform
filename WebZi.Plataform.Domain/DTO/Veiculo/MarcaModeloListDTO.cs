using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Veiculo
{
    public class MarcaModeloListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<MarcaModeloDTO> Listagem { get; set; } = new();
    }
}
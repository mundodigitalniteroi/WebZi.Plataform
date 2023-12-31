using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Veiculo
{
    public class TipoVeiculoListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<TipoVeiculoDTO> Listagem { get; set; } = new();
    }
}
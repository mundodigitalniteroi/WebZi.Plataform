using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Veiculo
{
    public class EquipamentoOpcionalListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<EquipamentoOpcionalDTO> Listagem { get; set; } = new();
    }
}
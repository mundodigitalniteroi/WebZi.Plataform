using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Deposito
{
    public class DepositoListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<DepositoDTO> Listagem { get; set; } = new();
    }
}
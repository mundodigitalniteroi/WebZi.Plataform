using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Deposito
{
    public class DepositoVincularAUsuariosListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<DepositoVincularAUsuariosDTO> Listagem { get; set; } = new();
    }
}
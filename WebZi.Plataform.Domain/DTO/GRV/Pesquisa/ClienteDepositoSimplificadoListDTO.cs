using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class ClienteDepositoSimplificadoListDTO
    {
        public MensagemDTO Mensagem { get; set; }

        public List<ClienteDepositoSimplificadoDTO> Listagem { get; set; } = new();
    }
}
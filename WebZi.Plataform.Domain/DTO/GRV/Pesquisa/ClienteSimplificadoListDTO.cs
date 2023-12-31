using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class ClienteSimplificadoListDTO
    {
        public MensagemDTO Mensagem { get; set; }

        public List<ClienteSimplificadoDTO> Listagem { get; set; } = new();
    }
}
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Cliente
{
    public class ClienteListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<ClienteDTO> Listagem { get; set; } = new();
    }
}
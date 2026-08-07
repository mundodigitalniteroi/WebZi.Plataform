using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Cliente
{
    public class ClienteVincularAUsuarioListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<ClienteVincularUsuarioDTO> Listagem { get; set; } = new();
    }
}
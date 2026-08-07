using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario
{
    public class UsuarioPorNomeOuLoginListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();
        public List<UsuarioPorNomeOuLoginDTO> Listagem { get; set; }
    }
}
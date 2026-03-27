
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario
{
    public class PerfisAcessoUsuarioListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<PerfisAcessoUsuarioDTO> Listagem { get; set; }
    }
}

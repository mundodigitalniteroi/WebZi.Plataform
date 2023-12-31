using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario
{
    public class UsuarioClienteDepositoReboquistaListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<UsuarioClienteDepositoReboquistaDTO> Listagem { get; set; } = new();
    }
}
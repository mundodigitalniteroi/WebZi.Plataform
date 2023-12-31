using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario
{
    public class UsuarioClienteDepositoReboqueListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<UsuarioClienteDepositoReboqueDTO> Listagem { get; set; } = new();
    }
}
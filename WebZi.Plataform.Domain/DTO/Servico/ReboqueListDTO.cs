using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Servico
{
    public class ReboqueListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<ReboqueDTO> Listagem { get; set; } = new();
    }
}
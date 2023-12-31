using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Servico
{
    public class ReboquistaListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<ReboquistaDTO> Listagem { get; set; } = new();
    }
}
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Vistoria
{
    public class VistoriaStatusListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<VistoriaStatusDTO> Listagem { get; set; } = new();
    }
}
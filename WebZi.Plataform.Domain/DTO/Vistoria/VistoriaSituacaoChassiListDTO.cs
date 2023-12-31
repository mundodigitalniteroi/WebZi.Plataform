using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Vistoria
{
    public class VistoriaSituacaoChassiListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<VistoriaSituacaoChassiDTO> Listagem { get; set; } = new();
    }
}
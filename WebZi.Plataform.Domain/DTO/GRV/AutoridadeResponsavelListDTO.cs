using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class AutoridadeResponsavelViewModelList
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<AutoridadeResponsavelDTO> Listagem { get; set; } = new();
    }
}
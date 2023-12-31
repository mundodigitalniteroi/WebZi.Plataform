using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class StatusAssinaturaCondutorViewModelList
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<StatusAssinaturaCondutorDTO> Listagem { get; set; } = new();
    }
}
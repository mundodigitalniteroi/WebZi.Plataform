using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class LacreViewModelList
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<LacreDTO> Listagem { get; set; } = new();
    }
}
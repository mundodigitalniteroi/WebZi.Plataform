using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class GrvViewModelList
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<GrvDTO> Listagem { get; set; } = new();
    }
}
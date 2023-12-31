using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class StatusOperacaoViewModelList
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<StatusOperacaoModel> Listagem { get; set; } = new();
    }
}
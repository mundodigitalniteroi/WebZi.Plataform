using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class GrvPesquisaResultListDTO
    {
        public MensagemDTO Mensagem { get; set; }

        public List<GrvPesquisaResultDTO> Listagem { get; set; } = new();
    }
}
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class ReboqueSimplificadoListDTO
    {
        public MensagemDTO Mensagem { get; set; }

        public List<ReboqueSimplificadoDTO> Listagem { get; set; } = new();
    }
}
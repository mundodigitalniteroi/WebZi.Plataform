using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class ReboquistaSimplificadoListDTO
    {
        public MensagemDTO Mensagem { get; set; }

        public List<ReboquistaSimplificadoDTO> Listagem { get; set; } = new();
    }
}
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class EnquadramentoInfracaoListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<EnquadramentoInfracaoDTO> Listagem { get; set; } = new();
    }
}
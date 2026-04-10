using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class EnquadramentoInfracaoGrvListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<EnquadramentoInfracaoGrvDTO> Listagem { get; set; }
    }
}
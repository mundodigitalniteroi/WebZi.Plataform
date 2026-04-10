using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class EnquadramentoInfracaoGrvDTO
    {
        private MensagemDTO Mensagem { get; set; } = new();
        public string NumeroInfracao { get; set; }
        public EnquadramentoInfracaoDTO Infracao { get; set; }
    }
}

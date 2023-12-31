using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Faturamento.Servico
{
    public class ServicoAssociadoGrvListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new MensagemDTO();

        public List<ServicoAssociadoGrvDTO> Listagem { get; set; } = new();
    }
}
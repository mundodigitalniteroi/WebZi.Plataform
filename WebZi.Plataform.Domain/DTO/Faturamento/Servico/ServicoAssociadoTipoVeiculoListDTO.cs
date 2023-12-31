using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Faturamento.Servico
{
    public class ServicoAssociadoTipoVeiculoListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new MensagemDTO();

        public List<ServicoAssociadoTipoVeiculoDTO> Listagem { get; set; } = new();
    }
}
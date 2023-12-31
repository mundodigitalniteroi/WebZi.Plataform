using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class FaturamentoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorFaturamento { get; set; }

        public List<FaturamentoComposicaoDTO> ListagemFaturamentoComposicao { get; set; }
    }
}
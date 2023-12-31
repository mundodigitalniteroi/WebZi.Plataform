using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class FaturamentoProdutoListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<FaturamentoProdutoDTO> Listagem { get; set; }
    }
}
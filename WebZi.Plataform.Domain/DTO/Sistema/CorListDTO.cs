namespace WebZi.Plataform.Domain.DTO.Sistema
{
    public class CorListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<CorDTO> Listagem { get; set; } = new();
    }
}
namespace WebZi.Plataform.Domain.DTO.Sistema
{
    public class TabelaGenericaListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new MensagemDTO();

        public List<TabelaGenericaDTO> Listagem { get; set; } = new();
    }
}
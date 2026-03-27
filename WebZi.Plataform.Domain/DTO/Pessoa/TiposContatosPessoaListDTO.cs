using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Pessoa
{
    public class TiposContatosPessoaListDTO
    {
        private MensagemDTO Mensagem { get; set; } = new();

        public List<TiposContatosPessoaDTO> Listagem { get; set; }
    }
}

using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Pessoa
{
    public class TiposContatosPessoaDTO
    {
        public int TipoContatoId { get; set; }

        public string TipoContato { get; set; }

        public string Contato { get; set; }

        public char FlagContatoPrincipal { get; set; }
    }
}
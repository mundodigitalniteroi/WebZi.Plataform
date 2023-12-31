using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Pessoa
{
    public class TipoDocumentoIdentificacaoListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<TipoDocumentoIdentificacaoDTO> Listagem { get; set; } = new();
    }
}
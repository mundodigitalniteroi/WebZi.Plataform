using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Pessoa
{
    public class TipoDocumentoIdentificacaoSimplificadoListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<TipoDocumentoIdentificacaoSimplificadoDTO> Listagem { get; set; } = new();
    }
}
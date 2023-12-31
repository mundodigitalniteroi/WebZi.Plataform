using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Atendimento
{
    public class QualificacaoResponsavelListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        // Não precisa criar uma ViewModel para esta entidade
        // porque são retornados todos os campos do Banco de Dados
        public List<QualificacaoResponsavelDTO> Listagem { get; set; } = new();
    }
}
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Documento
{
    public class OrgaoEmissorListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<OrgaoEmissorDTO> Listagem { get; set; } = new();
    }
}
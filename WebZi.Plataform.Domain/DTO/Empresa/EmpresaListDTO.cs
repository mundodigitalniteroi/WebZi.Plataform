using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Empresa
{
    public class EmpresaListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<EmpresaDTO> Listagem { get; set; } = new();
    }
}
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco
{
    public class BancoListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<BancoDTO> Listagem { get; set; } = new();
    }
}
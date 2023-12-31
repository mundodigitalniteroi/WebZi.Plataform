using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco
{
    public class AgenciaBancariaListDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<AgenciaBancariaDTO> Listagem { get; set; } = new();
    }
}
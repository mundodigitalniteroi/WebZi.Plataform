using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class MotivoApreensaoViewModelList
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<MotivoApreensaoDTO> Listagem { get; set; } = new();
    }
}
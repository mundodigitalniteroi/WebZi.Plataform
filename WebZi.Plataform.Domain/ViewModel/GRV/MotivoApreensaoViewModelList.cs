using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class MotivoApreensaoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<MotivoApreensaoModel> MotivosApreensoes { get; set; } = new();
    }
}
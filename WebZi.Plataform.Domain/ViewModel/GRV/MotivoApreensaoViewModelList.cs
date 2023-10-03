using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class MotivoApreensaoViewModelList
    {
        public List<MotivoApreensaoModel> MotivosApreensoes { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}
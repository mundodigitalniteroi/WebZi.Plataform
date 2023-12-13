namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class MotivoApreensaoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<MotivoApreensaoViewModel> Listagem { get; set; } = new();
    }
}
namespace WebZi.Plataform.Domain.ViewModel.Vistoria
{
    public class VistoriaSituacaoChassiViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<VistoriaSituacaoChassiViewModel> Listagem { get; set; } = new();
    }
}
namespace WebZi.Plataform.Domain.ViewModel.Vistoria
{
    public class VistoriaSituacaoChassiViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<VistoriaSituacaoChassiViewModel> ListagemSituacaoChassi { get; set; } = new();
    }
}
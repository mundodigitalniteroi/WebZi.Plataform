namespace WebZi.Plataform.Domain.ViewModel.Generic
{
    public class ImageViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new MensagemViewModel();

        public List<ImageViewModel> Listagem { get; set; }
    }
}
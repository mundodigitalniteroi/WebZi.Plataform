namespace WebZi.Plataform.Domain.ViewModel.Generic
{
    public class ImageViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new MensagemViewModel();

        public byte[] Imagem { get; set; }
    }
}
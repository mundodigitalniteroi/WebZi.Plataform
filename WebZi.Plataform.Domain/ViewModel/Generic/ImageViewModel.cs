namespace WebZi.Plataform.Domain.ViewModel.Generic
{
    public class ImageViewModel
    {
        public byte[] Imagem { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new MensagemViewModel();
    }
}
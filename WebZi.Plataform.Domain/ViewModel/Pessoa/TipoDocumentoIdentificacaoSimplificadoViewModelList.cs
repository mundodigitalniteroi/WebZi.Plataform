namespace WebZi.Plataform.Domain.ViewModel.Pessoa
{
    public class TipoDocumentoIdentificacaoSimplificadoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<TipoDocumentoIdentificacaoSimplificadoViewModel> Listagem { get; set; } = new();
    }
}
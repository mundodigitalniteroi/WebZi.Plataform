namespace WebZi.Plataform.Domain.ViewModel.Pessoa
{
    public class TipoDocumentoIdentificacaoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<TipoDocumentoIdentificacaoViewModel> Listagem { get; set; } = new();
    }
}
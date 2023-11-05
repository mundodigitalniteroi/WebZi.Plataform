namespace WebZi.Plataform.Domain.ViewModel.Documento
{
    public class OrgaoEmissorViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<OrgaoEmissorViewModel> Listagem { get; set; } = new();
    }
}
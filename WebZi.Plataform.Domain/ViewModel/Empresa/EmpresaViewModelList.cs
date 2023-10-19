namespace WebZi.Plataform.Domain.ViewModel.Empresa
{
    public class EmpresaViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<EmpresaViewModel> ListagemEmpresa { get; set; } = new();
    }
}
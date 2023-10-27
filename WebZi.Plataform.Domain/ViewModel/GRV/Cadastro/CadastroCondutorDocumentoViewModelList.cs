namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CadastroCondutorDocumentoViewModelList
    {
        public int IdentificadorGrv { get; set; }

        public int IdentificadorUsuario { get; set; }

        public List<CadastroCondutorDocumentoViewModel> ListagemDocumentoCondutor { get; set; }
    }
}
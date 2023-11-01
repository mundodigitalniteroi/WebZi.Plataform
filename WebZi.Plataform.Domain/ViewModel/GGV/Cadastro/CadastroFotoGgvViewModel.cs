namespace WebZi.Plataform.Domain.ViewModel.GGV.Cadastro
{
    public class CadastroFotoGgvViewModel
    {
        public int IdentificadorGrv { get; set; }

        public int IdentificadorUsuario { get; set; }

        public List<CadastroFotoTipoCadastroViewModel> Listagem { get; set; }
    }
}
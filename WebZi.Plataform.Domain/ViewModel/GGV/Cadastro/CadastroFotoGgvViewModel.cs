namespace WebZi.Plataform.Domain.ViewModel.GGV.Cadastro
{
    public class CadastroFotoGgvViewModel
    {
        //[Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorGrv { get; set; }

        //[Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        //[Required(ErrorMessage = "Propriedade obrigatória")]
        public List<CadastroFotoTipoCadastroViewModel> ListagemFotos { get; set; }
    }
}
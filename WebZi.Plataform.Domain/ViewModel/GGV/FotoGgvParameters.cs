namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class FotoGgvParameters
    {
        //[Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        //[Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        //[Required(ErrorMessage = "Propriedade obrigatória")]
        public List<FotoTipoCadastroParameters> ListagemFotos { get; set; }
    }
}
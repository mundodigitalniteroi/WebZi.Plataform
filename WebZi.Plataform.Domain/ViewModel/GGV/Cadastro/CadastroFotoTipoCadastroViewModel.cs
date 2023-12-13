namespace WebZi.Plataform.Domain.ViewModel.GGV.Cadastro
{
    public class CadastroFotoTipoCadastroViewModel
    {
        //[Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorTipoCadastro { get; set; }

        //[Required(ErrorMessage = "Propriedade obrigatória")]
        public byte[] Foto { get; set; }
    }
}
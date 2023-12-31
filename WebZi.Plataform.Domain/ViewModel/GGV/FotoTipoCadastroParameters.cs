namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class FotoTipoCadastroParameters
    {
        //[Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorTipoCadastro { get; set; }

        //[Required(ErrorMessage = "Propriedade obrigatória")]
        public byte[] Foto { get; set; }
    }
}
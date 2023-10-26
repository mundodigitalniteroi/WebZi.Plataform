namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CadastroFotoVeiculoViewModel
    {
        public int IdentificadorGrv { get; set; }

        public int IdentificadorUsuario { get; set; }

        public List<byte[]> Fotos { get; set; }
    }
}
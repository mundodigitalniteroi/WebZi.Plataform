namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class GrvFotoViewModel
    {
        public int IdentificadorGrv { get; set; }

        public int IdentificadorUsuario { get; set; }

        public List<byte[]> Fotos { get; set; }
    }
}
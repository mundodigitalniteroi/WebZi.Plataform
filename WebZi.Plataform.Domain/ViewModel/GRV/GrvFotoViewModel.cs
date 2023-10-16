namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class GrvFotoViewModel
    {
        public int GrvId { get; set; }

        public int UsuarioId { get; set; }

        public List<byte[]> Fotos { get; set; }
    }
}
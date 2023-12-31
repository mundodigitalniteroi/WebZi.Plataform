using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class FotoGrvParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public List<byte[]> Fotos { get; set; }
    }
}
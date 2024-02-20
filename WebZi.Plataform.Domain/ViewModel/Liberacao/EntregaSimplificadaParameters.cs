using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Liberacao
{
    public class EntregaSimplificadaParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        public byte[] ResponsavelFoto { get; set; }
    }
}
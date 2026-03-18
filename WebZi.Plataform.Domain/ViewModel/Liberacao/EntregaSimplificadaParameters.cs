using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Liberacao
{
    public class EntregaSimplificadaParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public byte IdentificadorTipoLiberacao { get; set; }
        public int? IdentificadorSaidaReparo { get; set; }

        public byte[] ResponsavelFoto { get; set; }
    }
}
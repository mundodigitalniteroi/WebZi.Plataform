using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CadastroFotoGrvViewModel
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorGrv { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public List<byte[]> Fotos { get; set; }
    }
}
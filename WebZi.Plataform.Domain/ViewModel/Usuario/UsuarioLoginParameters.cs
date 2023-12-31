using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Usuario
{
    public class UsuarioLoginParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public string Senha { get; set; }
    }
}
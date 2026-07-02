using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Usuario;

public class GerarCodigoMfaParameters
{
    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int UsuarioId { get; set; }
}
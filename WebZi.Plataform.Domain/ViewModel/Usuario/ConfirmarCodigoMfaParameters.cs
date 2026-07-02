using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebZi.Plataform.Domain.ViewModel.Usuario;

public class ConfirmarCodigoMfaParameters
{
    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    [StringLength(maximumLength: 6, MinimumLength = 6)]
    public string Codigo { get; set; }
}
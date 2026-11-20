using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.Usuario;

public class ConfirmarCodigoMfaParameters
{
    [JsonIgnore]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    [StringLength(maximumLength: 6, MinimumLength = 6)]
    public string Codigo { get; set; }
}
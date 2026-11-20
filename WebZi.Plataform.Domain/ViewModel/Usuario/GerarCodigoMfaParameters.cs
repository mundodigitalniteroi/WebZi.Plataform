using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.Usuario;

public class GerarCodigoMfaParameters
{
    [JsonIgnore]
    public int UsuarioId { get; set; }
}
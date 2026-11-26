using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.Transalvador.Entrada;

public class ReenviarVeiculoApreendidoParameters
{
    [JsonIgnore] public int identificadorUsuario { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int identificadorCliente { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int identificadorDeposito { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    [StringLength(15, ErrorMessage = "{0} Não pode ter mais que {2} caracteris")]
    public string NumeroProcesso { get; set; }
}
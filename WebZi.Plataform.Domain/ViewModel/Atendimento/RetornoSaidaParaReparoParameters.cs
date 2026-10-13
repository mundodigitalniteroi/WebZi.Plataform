using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento;

public class RetornoSaidaParaReparoParameters
{
    [Required(ErrorMessage = "Propriedade Requerida")]
    public int IdentificadorAtendimento { get; set; }

    [Required(ErrorMessage = "Propriedade Requerida")]
    public int IdentificadorSaidaReparo { get; set; }
    
    [Required(ErrorMessage = "Propriedade Requerida")]
    public int IdentificadorUsuario { get; set; }
    
}
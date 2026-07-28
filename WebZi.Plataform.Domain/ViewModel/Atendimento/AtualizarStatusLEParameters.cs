using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento;

public class AtualizarStatusLEParameters
{
    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int IdentificadorProcesso { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int IdentificadorUsuario { get; set; }
}
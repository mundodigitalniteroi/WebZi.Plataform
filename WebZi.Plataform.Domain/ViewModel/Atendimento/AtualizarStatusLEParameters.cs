using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento;

public class AtualizarStatusLEParameters
{
    [Required(ErrorMessage = "Propriedade obrigatória")]
    public long IdentificadorProcesso { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    public long IdentificadorUsuario { get; set; }
}
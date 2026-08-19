using System.ComponentModel.DataAnnotations;
using WebZi.Plataform.Domain.ViewModel.Usuario.CadastroUsuario;

namespace WebZi.Plataform.Domain.ViewModel.Usuario.AtualizarUsuario;

public class AtualizarUsuarioParameters
{
    [MaxLength(20, ErrorMessage = "{0} deve ter no máximo {1} caracteres.")]
    public string Login { get; set; }

    public long identificadorPessoa { get; set; }

    public string? NomePessoa { get; set; }

    [MaxLength(15, ErrorMessage = "{0} deve ter no máximo {1} caracteres.")]
    public string Matricula { get; set; }
    [Required(ErrorMessage = "Propriedade obrigatória")]
    [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
    public char FlagMfa { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
    public string FlagPermissaoDesconto { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
    public string FlagPermissaoDataRetroativaFaturamento { get; set; }
    public List<int>? PermissoesUsuario { get; set; }
    public List<int> PerfisDeAcesso { get; set; }
    public List<int> VincularCliente { get; set; }
    public List<int> VincularDeposito { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
    public string FlagAtivo { get; set; }
}
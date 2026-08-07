using System.ComponentModel.DataAnnotations;
using WebZi.Plataform.Domain.ViewModel.Usuario.CadastroUsuario;

namespace WebZi.Plataform.Domain.ViewModel.Usuario.AtualizarUsuario;

public class AtualizarUsuarioParameters
{
    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int identificadorUsuario { get; set; }

    [MaxLength(20, ErrorMessage = "{0} deve ter no máximo {1} caracteres.")]
    public string Login { get; set; }

    public long identificadorPessoa { get; set; }

    [MaxLength(15, ErrorMessage = "{0} deve ter no máximo {1} caracteres.")]
    public string Matricula { get; set; }

    public List<PermissoesUsuarioParameters> PermissoesUsuario { get; set; }

    public List<int> PerfisDeAcesso { get; set; }
    public List<int> VincularCliente { get; set; }
    public List<int> VincularDeposito { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
    public string FlagAtivo { get; set; }
}
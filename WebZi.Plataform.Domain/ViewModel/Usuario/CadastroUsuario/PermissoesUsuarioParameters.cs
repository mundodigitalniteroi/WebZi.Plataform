using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Usuario.CadastroUsuario;

public class PermissoesUsuarioParameters
{
    public int identificadorTipoPermissao { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
    public string FlagPermissaoDesconto { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
    public string FlagPermissaoDataRetroativaFaturamento { get; set; }
}
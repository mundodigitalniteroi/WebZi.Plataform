using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class EquipamentoOpcionalParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public decimal IdentificadorEquipamentoOpcional { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagPossuiEquipamento { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagEquipamentoAvariado { get; set; }

        public int? IdentificadorTipoAvaria { get; set; }
    }
}
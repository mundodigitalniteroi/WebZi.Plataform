using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class CondutorEquipamentoOpcionalParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [Range(1, 999)]
        public decimal IdentificadorEquipamentoOpcional { get; set; }

        public int? IdentificadorUsuarioCadastro { get; set; }

        public int? CodigoAvaria { get; set; }

        public string Avariado { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagPossuiEquipamento { get; set; }
    }
}
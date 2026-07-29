using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class FaturamentoServicoGrvParameters
    {
        // [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorServicoAssociadoTipoVeiculo { get; set; }

        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        [StringLength(1)]
        public string FlagCobranca { get; set; }

        // [Required(ErrorMessage = "Propriedade obrigatória")]
        public int? Quantidade { get; set; }

        // [Required(ErrorMessage = "Propriedade obrigatória")]
        // [MaxLength(12)]
        public string ValorTipoCobrancaInformado { get; set; }

        [StringLength(5, ErrorMessage = "Tamanho máximo de 5 caracteres (HH:MM)")]
        public string HoraMinuto { get; set; }
    }
}
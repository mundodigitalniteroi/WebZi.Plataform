using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CadastroCondutorViewModel
    {
        [MaxLength(20)]
        public string Documento { get; set; }

        [MaxLength(20)]
        public string Identidade { get; set; }

        [MaxLength(10)]
        public string OrgaoExpedidor { get; set; }

        [MaxLength(150)]
        public string Nome { get; set; }

        [MaxLength(9)]
        public string Telefone { get; set; }

        [StringLength(2, MinimumLength = 2)]
        public string TelefoneDDD { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(6)]
        public string NumeroChaveVeiculo { get; set; }

        [MaxLength(1000)]
        public string InformacoesAdicionais { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorAssinaturaCondutor { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagChaveVeiculo { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagDocumentacaoVeiculo { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class SaidaParaReparoParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorAtendimento { get; set; }
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public DateTime DataSaida { get; set; }
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public DateTime DataPrevisaoRetorno { get; set; }
        [Required(ErrorMessage = "Propriedade obrigatória")]
        [StringLength(500, ErrorMessage = "Valor maximo passado")]
        public string MotivoSaida { get; set; }
    }
}

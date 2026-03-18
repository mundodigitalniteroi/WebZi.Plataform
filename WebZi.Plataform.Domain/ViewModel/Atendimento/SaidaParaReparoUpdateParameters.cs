using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class SaidaParaReparoUpdateParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorAtendimento { get; set; }
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorSaidaParaReparo { get; set; }
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public DateTime DataPrevisaoRetorno { get; set; }
    }
}

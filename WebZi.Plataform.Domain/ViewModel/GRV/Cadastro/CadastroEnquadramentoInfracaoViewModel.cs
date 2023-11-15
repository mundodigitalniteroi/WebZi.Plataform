using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CadastroEnquadramentoInfracaoViewModel
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public decimal IdentificadorEnquadramentoInfracao { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [MaxLength(20)]
        public string NumeroInfracao { get; set; }
    }
}
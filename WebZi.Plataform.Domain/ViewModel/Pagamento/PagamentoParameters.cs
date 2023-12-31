using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Pagamento
{
    public class PagamentoParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorFaturamento { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }
    }
}
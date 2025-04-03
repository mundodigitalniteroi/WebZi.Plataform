using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace WebZi.Plataform.Domain.ViewModel.Pagamento
{
    public class PagamentoParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorFaturamento { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }
        public PagamentoParameterCartao Cartao { get; set; }
    }

    public class PagamentoParameterCartao
    {
        public int Bandeira { get; set; }
        public string CodigoAutorizacao { get; set; }
        public string NumeroCartao { get; set; }
    }
}
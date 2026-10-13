using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class GerarFaturamentoAdicionalViewModel
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        public int? IdentificadorSaidaReparo { get; set; }
    }
}

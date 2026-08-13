using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class GerarFaturamentoSaidaReparoViewModel
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorSaidaReparo { get; set; }

    }
}

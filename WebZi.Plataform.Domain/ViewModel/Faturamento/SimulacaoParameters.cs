using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class SimulacaoParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public string CodigoProduto { get; set; }

        public int IdentificadorProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorCliente { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorDeposito { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        public string Placa { get; set; }

        public string Chassi { get; set; }

        public DateTime? DataHoraInicialParaCalculo { get; set; }

        public DateTime? DataHoraFinalParaCalculo { get; set; }

        public bool IsComboio { get; set; }
    }
}
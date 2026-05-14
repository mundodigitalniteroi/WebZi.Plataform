using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class LacreParameters
    {
        public int? IdentificadorLacre { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [MaxLength(20)]
        public string Lacre { get; set; }
    }
}

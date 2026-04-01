using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Reboque
{
    public class CadastrarReboqueParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorCliente { get; set; }
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorDeposito { get; set; }
        [Required(ErrorMessage = "Propriedade obrigatória")]
        [StringLength(4, ErrorMessage = "O código deve conter no máximo 4 caracteres.")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Propriedade obrigatória")]
        [StringLength(8, ErrorMessage = "A placa deve conter no máximo 8 caracteres.")]
        public string? Placa { get; set; }
        public string? Chassi { get; set; }
        public string? Renavam { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Ano { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CadastrarReboquistaParameters
    {
        [JsonIgnore]
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorCliente { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorDeposito { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [StringLength(100, ErrorMessage = "O nome do reboquista deve conter no máximo 100 caracteres.")]
        public string NomeReboquista { get; set; }
    }
}
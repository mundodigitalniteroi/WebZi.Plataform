using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CondutorDocumentoParametersList
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        public List<CondutorDocumentoParameters> ListagemDocumentoCondutor { get; set; }
    }
}
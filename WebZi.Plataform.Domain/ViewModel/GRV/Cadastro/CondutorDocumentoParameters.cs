using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CondutorDocumentoParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public byte IdentificadorTipoDocumentoIdentificacao { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public byte[] Imagem { get; set; }
    }
}
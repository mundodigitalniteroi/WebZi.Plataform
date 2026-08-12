using System.ComponentModel.DataAnnotations;
using WebZi.Plataform.Domain.ViewModel.Atendimento;

namespace WebZi.Plataform.Domain.ViewModel.Liberacao
{
    public class EntregaParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorAtendimento { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public byte IdentificadorTipoLiberacao { get; set; }

        public int? IdentificadorSaidaReparo { get; set; }
        public FormaLiberacaoParameters? FormaLiberacao { get; set; }
        public LiberacaoEspecialParameters? LiberacaoEspecial { get; set; }
        
        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagProcessarFaturamentoAdicional { get; set; }
        public byte[] ResponsavelFoto { get; set; }
    }
}
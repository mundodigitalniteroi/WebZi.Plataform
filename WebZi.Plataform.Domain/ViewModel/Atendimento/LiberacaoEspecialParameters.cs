using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class LiberacaoEspecialParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorFaturamento { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        [Required(ErrorMessage = "Tipo de liberação precisa ser passado")]
        public byte IdLiberacaoEspecialTipo { get; set; }

        [Required(ErrorMessage = "Numero documento precisa ser preenchido")]
        [MaxLength(20, ErrorMessage = "Não pode ser maior que 20 caracteres o Numero do Documento")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "Tipo documento precisa ser preenchido")]
        [MaxLength(20, ErrorMessage = "Não pode ser maior que 20 caracteres o Tipo do Documento")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "Numero do processo precisa ser preenchido")]
        [MaxLength(20, ErrorMessage = "Não pode ser maior que 20 caracteres o Numero do Processo")]
        public string NumeroProcesso { get; set; }

        public string OrgaoEmissor { get; set; }
        public string PortadorNome { get; set; }
        public string PortadorCargo { get; set; }
        public string PortadorMatricula { get; set; }
        public string SignatarioNomeDocumento { get; set; }
        public string SignatarioMatricula { get; set; }
        public string SignatarioTitulo { get; set; }
        [Required] public DateTime DataEmissaoDocumento { get; set; }
    }
}
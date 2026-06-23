using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class AtualizarLiberacaoEspecialParameters
    {
        [Required] public int IdGrv { get; set; }
        public int? IdFaturamento { get; set; }
        public byte IdLiberacaoEspecialTipo { get; set; }
        public int? IdUsuarioCadastro { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroProcesso{ get; set; }
        public string OrgaoEmissor { get; set; }
        public string PortadorNome { get; set; }
        public string PortadorCargo{ get; set; }
        public string PortadorMatricula { get; set; }
        public string SignatarioNomeDocumento { get; set; }
        public string SignatarioMatricula { get; set; }
        public string SignatarioTitulo { get; set; }
        [Required] public DateTime DataEmissaoDocumento { get; set; }
    }
}

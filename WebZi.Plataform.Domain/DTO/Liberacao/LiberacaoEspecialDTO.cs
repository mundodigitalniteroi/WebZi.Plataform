
namespace WebZi.Plataform.Domain.DTO.Liberacao
{
    public class LiberacaoEspecialDTO
    {
        public byte IdLiberacaoEspecialTipo { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroProcesso { get; set; }
        public string OrgaoEmissor { get; set; }
        public string PortadorNome { get; set; }
        public string PortadorCargo { get; set; }
        public string PortadorMatricula { get; set; }
        public string SignatarioNomeDocumento { get; set; }
        public string SignatarioMatricula { get; set; }
        public string SignatarioTitulo { get; set; }
        public DateTime DataEmissaoDocumento { get; set; }
        public decimal Valor { get; set; }
    }
}

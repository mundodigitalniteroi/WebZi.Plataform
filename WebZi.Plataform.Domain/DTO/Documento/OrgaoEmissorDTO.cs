namespace WebZi.Plataform.Domain.DTO.Documento
{
    public class OrgaoEmissorDTO
    {
        public short IdentificadorOrgaoEmissor { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }

        public string UF { get; set; }

        public string FlagAutoridadeResponsavel { get; set; }

        public string FlagDetran { get; set; }

        public string FlagAtivo { get; set; }
    }
}
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Documento
{
    public class OrgaoEmissorModel
    {
        public short OrgaoEmissorId { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get; set; }

        public string UF { get; set; }

        public string CodigoOrgao { get; set; }

        public string FlagAutoridadeResponsavel { get; set; } = "N";

        public string FlagDetran { get; set; } = "N";

        public string FlagAtivo { get; set; } = "S";

        public virtual ICollection<AutoridadeResponsavelModel> AutoridadesResponsaveis { get; set; }

        // public virtual ICollection<TbGloPesPessoasDocumentosIdentificacao> TbGloPesPessoasDocumentosIdentificacaos { get; set; } = new List<TbGloPesPessoasDocumentosIdentificacao>();

        // public virtual EstadoModel Estado { get; set; }
    }
}
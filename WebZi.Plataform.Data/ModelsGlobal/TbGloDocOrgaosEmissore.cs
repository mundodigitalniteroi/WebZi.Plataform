using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloDocOrgaosEmissore
{
    public short IdOrgaoEmissor { get; set; }

    public string Sigla { get; set; }

    public string Descricao { get; set; }

    public string Uf { get; set; }

    public string FlagAutoridadeResponsavel { get; set; }

    public string FlagDetran { get; set; }

    public string FlagAtivo { get; set; }

    public string CodigoOrgao { get; set; }

    public virtual ICollection<TbGloPesPessoasDocumentosIdentificacao> TbGloPesPessoasDocumentosIdentificacaos { get; set; } = new List<TbGloPesPessoasDocumentosIdentificacao>();

    public virtual TbGloLocEstado UfNavigation { get; set; }
}

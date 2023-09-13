using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloPesPessoasDocumentosIdentificacao
{
    public long IdPessoaDocumentoIdentificacao { get; set; }

    public long IdPessoa { get; set; }

    public short IdOrgaoEmissor { get; set; }

    public byte IdTipoDocumentoIdentificacao { get; set; }

    public string Descricao { get; set; }

    public DateTime? DataEmissao { get; set; }

    public DateTime? DataValidade { get; set; }

    public string Complemento { get; set; }

    public virtual TbGloDocOrgaosEmissore IdOrgaoEmissorNavigation { get; set; }

    public virtual TbGloPesPessoa IdPessoaNavigation { get; set; }

    public virtual TbGloDocTiposDocumentosIdentificacao IdTipoDocumentoIdentificacaoNavigation { get; set; }
}

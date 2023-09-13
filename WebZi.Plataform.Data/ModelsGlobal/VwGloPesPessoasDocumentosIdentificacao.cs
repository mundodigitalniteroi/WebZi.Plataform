using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class VwGloPesPessoasDocumentosIdentificacao
{
    public long IdPessoaDocumentoIdentificacao { get; set; }

    public long IdPessoa { get; set; }

    public string PessoasDocumentosIdentificacaoDocumento { get; set; }

    public string PessoasDocumentosIdentificacaoDataEmissao { get; set; }

    public string PessoasDocumentosIdentificacaoDataValidade { get; set; }

    public string PessoasDocumentosIdentificacaoComplemento { get; set; }

    public byte IdTipoDocumentoIdentificacao { get; set; }

    public string TiposDocumentosIdentificacaoCodigo { get; set; }

    public string TiposDocumentosIdentificacaoDescricao { get; set; }

    public string TiposDocumentosIdentificacaoFormato { get; set; }

    public byte TiposDocumentosIdentificacaoTamanhoMinimo { get; set; }

    public byte TiposDocumentosIdentificacaoTamanhoMaximo { get; set; }

    public byte TiposDocumentosIdentificacaoOrdemApresentacao { get; set; }

    public string TiposDocumentosIdentificacaoFlagPrincipal { get; set; }

    public string TiposDocumentosIdentificacaoFlagPossuiDataEmissao { get; set; }

    public string TiposDocumentosIdentificacaoFlagPossuiDataValidade { get; set; }

    public string TiposDocumentosIdentificacaoFlagPossuiComplemento { get; set; }

    public string TiposDocumentosIdentificacaoFlagAtivo { get; set; }

    public short IdOrgaoEmissor { get; set; }

    public string OrgaosEmissoresSigla { get; set; }

    public string OrgaosEmissoresDescricao { get; set; }

    public string OrgaosEmissoresUf { get; set; }

    public string OrgaosEmissoresFlagAutoridadeResponsavel { get; set; }

    public string OrgaosEmissoresFlagAtivo { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloDocTiposDocumentosIdentificacao
{
    public byte IdTipoDocumentoIdentificacao { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public string Formato { get; set; }

    public byte TamanhoMinimo { get; set; }

    public byte TamanhoMaximo { get; set; }

    public byte OrdemApresentacao { get; set; }

    public string FlagPrincipal { get; set; }

    public string FlagPossuiDataEmissao { get; set; }

    public string FlagPossuiDataValidade { get; set; }

    public string FlagPossuiComplemento { get; set; }

    public string FlagAtivo { get; set; }

    public virtual ICollection<TbGloPesPessoasDocumentosIdentificacao> TbGloPesPessoasDocumentosIdentificacaos { get; set; } = new List<TbGloPesPessoasDocumentosIdentificacao>();
}

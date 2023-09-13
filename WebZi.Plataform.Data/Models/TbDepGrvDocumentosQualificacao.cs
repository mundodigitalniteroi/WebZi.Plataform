using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvDocumentosQualificacao
{
    public byte GrvDocumentosQualificacaoId { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepGrvDocumento> TbDepGrvDocumentos { get; set; } = new List<TbDepGrvDocumento>();
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSolicitacaoReboqueStatus
{
    public byte IdSolicitacaoReboqueStatus { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepSolicitacaoReboque> TbDepSolicitacaoReboques { get; set; } = new List<TbDepSolicitacaoReboque>();
}

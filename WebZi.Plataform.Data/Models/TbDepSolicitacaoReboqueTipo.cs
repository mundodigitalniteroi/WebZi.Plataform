using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSolicitacaoReboqueTipo
{
    public byte IdSolicitacaoReboqueTipo { get; set; }

    public byte? IdMotivoApreensao { get; set; }

    public string FaturamentoProdutoCodigo { get; set; }

    public string Descricao { get; set; }

    public virtual TbDepFaturamentoProduto FaturamentoProdutoCodigoNavigation { get; set; }

    public virtual TbDepGrvMotivoApreensao IdMotivoApreensaoNavigation { get; set; }

    public virtual ICollection<TbDepSolicitacaoReboque> TbDepSolicitacaoReboques { get; set; } = new List<TbDepSolicitacaoReboque>();
}

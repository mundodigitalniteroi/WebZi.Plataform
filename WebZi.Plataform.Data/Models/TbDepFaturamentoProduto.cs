using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoProduto
{
    public string FaturamentoProdutoCodigo { get; set; }

    public string Descricao { get; set; }

    public string FlagSolicitacaoReboque { get; set; }

    public virtual ICollection<TbDepFaturamentoServicosTipo> TbDepFaturamentoServicosTipos { get; set; } = new List<TbDepFaturamentoServicosTipo>();

    public virtual ICollection<TbDepGrv> TbDepGrvs { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepSolicitacaoReboqueTipo> TbDepSolicitacaoReboqueTipos { get; set; } = new List<TbDepSolicitacaoReboqueTipo>();
}

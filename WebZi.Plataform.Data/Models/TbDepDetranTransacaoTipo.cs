using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetranTransacaoTipo
{
    public byte IdTransacaoTipo { get; set; }

    public string Codigo { get; set; }

    public string DescricaoTipo { get; set; }

    public string CodigoTransacao { get; set; }

    public virtual ICollection<TbDepDetranTransacaoStatus> TbDepDetranTransacaoStatuses { get; set; } = new List<TbDepDetranTransacaoStatus>();
}

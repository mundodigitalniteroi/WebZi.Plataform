using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetranTransacaoStatus
{
    public byte IdTransacaoStatus { get; set; }

    public byte IdTransacaoTipo { get; set; }

    public string DescricaoStatus { get; set; }

    public string TipoEvento { get; set; }

    public virtual TbDepDetranTransacaoTipo IdTransacaoTipoNavigation { get; set; }

    public virtual ICollection<TbDepDetranAssociacaoTransacaoClienteDeposito> TbDepDetranAssociacaoTransacaoClienteDepositos { get; set; } = new List<TbDepDetranAssociacaoTransacaoClienteDeposito>();
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetranAssociacaoTransacaoClienteDeposito
{
    public short IdTransacaoClienteDeposito { get; set; }

    public byte IdTransacaoStatus { get; set; }

    public int IdClienteDeposito { get; set; }

    public byte Ordenacao { get; set; }

    public string FlagObrigatorio { get; set; }

    public virtual TbDepClientesDeposito IdClienteDepositoNavigation { get; set; }

    public virtual TbDepDetranTransacaoStatus IdTransacaoStatusNavigation { get; set; }

    public virtual ICollection<TbDepDetranGrvStatusTransacao> TbDepDetranGrvStatusTransacaos { get; set; } = new List<TbDepDetranGrvStatusTransacao>();
}

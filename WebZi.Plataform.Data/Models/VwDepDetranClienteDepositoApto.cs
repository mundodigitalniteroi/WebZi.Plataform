using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepDetranClienteDepositoApto
{
    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int IdClienteDeposito { get; set; }

    public short? IdOrgaoEmissor { get; set; }

    public byte IdTransacaoTipo { get; set; }

    public byte IdTransacaoStatus { get; set; }

    public short IdTransacaoClienteDeposito { get; set; }

    public string TransacaoClienteDepositoFlagObrigatorio { get; set; }

    public string CodigoPatioDetro { get; set; }

    public string TipoCodigo { get; set; }

    public string TipoDescricao { get; set; }

    public string StatusDescricao { get; set; }

    public string StatusTipoEvento { get; set; }

    public byte TransacaoOrdenacao { get; set; }

    public string OrgaoEmissorUf { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepTarifasBasico
{
    public short IdCliente { get; set; }

    public string ClienteNome { get; set; }

    public string ClienteFlagAtivo { get; set; }

    public int IdClienteDeposito { get; set; }

    public string ClienteDepositoFlagAtivo { get; set; }

    public short IdDeposito { get; set; }

    public string DepositoNome { get; set; }

    public string DepositoFlagAtivo { get; set; }

    public int IdTarifa { get; set; }

    public string TarifaDescricao { get; set; }

    public DateTime DataVigenciaInicial { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }
}

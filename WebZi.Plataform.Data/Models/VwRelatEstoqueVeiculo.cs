using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwRelatEstoqueVeiculo
{
    public string NumeroFormularioGrv { get; set; }

    public string Cliente { get; set; }

    public string Deposito { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string MarcaModelo { get; set; }

    public string Cor { get; set; }

    public DateTime? DataHoraRemocao { get; set; }

    public string StatusOperacao { get; set; }

    public string Tipoveiculo { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }
}

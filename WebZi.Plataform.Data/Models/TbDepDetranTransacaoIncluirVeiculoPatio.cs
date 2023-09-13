using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetranTransacaoIncluirVeiculoPatio
{
    public int IdDetranTransacaoIncluirVeiculoPatio { get; set; }

    public int IdDetranGrvTransacao { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Operador { get; set; }

    public string NumeroGuiaRecolhimento { get; set; }

    public string UsoReboque { get; set; }

    public string DataGuarda { get; set; }

    public string HoraGuarda { get; set; }

    public int? CodigoPatio { get; set; }

    public virtual TbDepDetranGrvStatusTransacao IdDetranGrvTransacaoNavigation { get; set; }
}

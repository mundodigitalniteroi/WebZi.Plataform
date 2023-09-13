using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetranTransacaoRecolherVeiculo
{
    public int IdDetranTransacaoRecolherVeiculo { get; set; }

    public int IdDetranGrvTransacao { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Operador { get; set; }

    public string NumGuiaRecolhimento { get; set; }

    public string Uf { get; set; }

    public string DataRecolhimento { get; set; }

    public string HoraRecolhimento { get; set; }

    public string Classificacao { get; set; }

    public string Condicionalidade { get; set; }

    public virtual TbDepDetranGrvStatusTransacao IdDetranGrvTransacaoNavigation { get; set; }
}

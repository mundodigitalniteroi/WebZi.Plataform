using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetranTransacaoConsultarVeiculo
{
    public int IdDetranTransacaoConsultarVeiculo { get; set; }

    public int IdDetranGrvTransacao { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Operador { get; set; }

    public virtual TbDepDetranGrvStatusTransacao IdDetranGrvTransacaoNavigation { get; set; }
}

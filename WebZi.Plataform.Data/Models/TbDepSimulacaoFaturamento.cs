using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSimulacaoFaturamento
{
    public int IdGrv { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string StatusOperacao { get; set; }

    public string Comboio { get; set; }

    public double? ComposicaoValorTotal { get; set; }

    public DateTime? DataHoraCalculo { get; set; }
}

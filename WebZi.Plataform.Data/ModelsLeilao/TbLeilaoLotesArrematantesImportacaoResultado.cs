using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoLotesArrematantesImportacaoResultado
{
    public int Id { get; set; }

    public int? IdImportacaoArrematantes { get; set; }

    public int? Linha { get; set; }

    public int? Coluna { get; set; }

    public string MsgErro { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataLoteBaixa
{
    public int AlterDataLoteBaixaId { get; set; }

    public int AlterDataTituloReceberId { get; set; }

    public string Identificador { get; set; }

    public string JsonEnvio { get; set; }

    public string JsonRetorno { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepAlterdataTituloReceber AlterDataTituloReceber { get; set; }
}

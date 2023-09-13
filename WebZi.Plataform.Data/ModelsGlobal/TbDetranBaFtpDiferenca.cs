using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbDetranBaFtpDiferenca
{
    public int IdFtpRetornoDetalhe { get; set; }

    public string NomeCampo { get; set; }

    public string ValorAnterior { get; set; }

    public string ValorAtual { get; set; }
}

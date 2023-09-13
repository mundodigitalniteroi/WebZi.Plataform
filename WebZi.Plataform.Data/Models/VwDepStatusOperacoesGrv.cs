using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepStatusOperacoesGrv
{
    public string IdStatusOperacao { get; set; }

    public string Descricao { get; set; }

    public string FlagVeiculoApreendido { get; set; }

    public byte? Sequencia { get; set; }
}

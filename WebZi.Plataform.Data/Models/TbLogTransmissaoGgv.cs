using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogTransmissaoGgv
{
    public long Id { get; set; }

    public string Retorno { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Transmitido { get; set; }
}

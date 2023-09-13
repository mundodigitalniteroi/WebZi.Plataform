using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataRetorno
{
    public int AlterDataRetornoId { get; set; }

    public string Envio { get; set; }

    public string Retorno { get; set; }

    public string Token { get; set; }

    public string Metodo { get; set; }

    public DateTime? DataCadastro { get; set; }
}

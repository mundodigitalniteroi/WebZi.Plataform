using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwReboquesTerceirizado
{
    public int IdReboqueTerceirizado { get; set; }

    public string Empresa { get; set; }

    public int? IdReboque { get; set; }

    public byte? IdTipoVeiculoClassificacaoNome { get; set; }

    public decimal? ValorTarifa { get; set; }
}

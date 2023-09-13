using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwLeilao060117v2BoletosEmitido
{
    public int IdBoleto { get; set; }

    public string CedenteNossoNumeroBoleto { get; set; }

    public DateTime DataCadastroBoleto { get; set; }

    public string BoletoVencimento { get; set; }

    public string BoletoValor { get; set; }

    public string Lote { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class VwEmailArrematacaoBoletosPago
{
    public int IdBoleto { get; set; }

    public string Email { get; set; }

    public string Assunto { get; set; }

    public string Corpo { get; set; }
}

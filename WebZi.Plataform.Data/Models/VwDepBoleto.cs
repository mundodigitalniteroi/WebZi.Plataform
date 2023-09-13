﻿using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepBoleto
{
    public int IdFaturamento { get; set; }

    public string CedenteNome { get; set; }

    public string CedenteDocumento { get; set; }

    public string CedenteBancoNome { get; set; }

    public string CedenteCodigoFebraban { get; set; }

    public string CedenteCodigo { get; set; }

    public string CedenteAgencia { get; set; }

    public string CedenteContaCorrente { get; set; }

    public string CedenteDv { get; set; }

    public string SacadoCarteira { get; set; }

    public string NumeroDocumento { get; set; }

    public string CedenteNossoNumero { get; set; }

    public string ValorBoleto { get; set; }

    public string Vencimento { get; set; }

    public string SacadoNome { get; set; }

    public string SacadoDocumento { get; set; }

    public string SacadoEndereco { get; set; }

    public string SacadoBairro { get; set; }

    public string SacadoCidade { get; set; }

    public string SacadoUf { get; set; }

    public string SacadoCep { get; set; }

    public string SacadoInstrucoes { get; set; }
}

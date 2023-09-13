using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwSapOrdensVendasYb02
{
    public int IdFaturamentoComposicao { get; set; }

    public int IdTransacaoSap { get; set; }

    public string IdDocumento { get; set; }

    public string Mensagens { get; set; }

    public string Nota { get; set; }

    public string TipoDocumento { get; set; }

    public DateTime DataCadastro { get; set; }
}

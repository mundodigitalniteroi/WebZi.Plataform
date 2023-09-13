using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwSapClientesRetorno
{
    public int IdSapClientes { get; set; }

    public int IdTransacaoSap { get; set; }

    public int IdGrv { get; set; }

    public string FlagErroWsSap { get; set; }

    public int IdSapRetorno { get; set; }

    public string IdDocumento { get; set; }

    public string Mensagens { get; set; }

    public string Nota { get; set; }

    public DateTime DataHoraRegistro { get; set; }
}

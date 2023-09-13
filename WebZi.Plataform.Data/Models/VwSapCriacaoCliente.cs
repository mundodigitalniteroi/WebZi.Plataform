using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwSapCriacaoCliente
{
    public int IdGrv { get; set; }

    public int IdTransacaoSap { get; set; }

    public string IdDocumento { get; set; }

    public string Mensagens { get; set; }

    public string Cpf { get; set; }

    public string Cnpj { get; set; }

    public DateTime DataHoraRegistro { get; set; }
}

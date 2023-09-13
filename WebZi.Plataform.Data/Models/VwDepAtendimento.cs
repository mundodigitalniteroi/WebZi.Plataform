using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepAtendimento
{
    public int IdGrv { get; set; }

    public int IdAtendimento { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public string IdStatusOperacao { get; set; }

    public string StatusDoGrv { get; set; }

    public string Cliente { get; set; }

    public string Depósito { get; set; }

    public string IdDocumentoSap { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string StatusCadastroSap { get; set; }

    public string StatusCadastroOrdensVendaSap { get; set; }
}

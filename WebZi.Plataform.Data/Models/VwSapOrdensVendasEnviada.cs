using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwSapOrdensVendasEnviada
{
    public int IdGrv { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public string IdStatusOperacao { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string ClienteNome { get; set; }

    public string DepositoNome { get; set; }

    public int IdAtendimento { get; set; }

    public string IdDocumentoSap { get; set; }

    public string StatusCadastroSap { get; set; }

    public string StatusCadastroOrdensVendaSap { get; set; }

    public string NotaFiscalNome { get; set; }

    public DateTime DataEnvioSap { get; set; }
}

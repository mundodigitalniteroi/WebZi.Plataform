using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepNfe
{
    public int NfeId { get; set; }

    public int GrvId { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public int IdentificadorNota { get; set; }

    public int? IdentificadorNotaOriginal { get; set; }

    public int? FaturamentoServicoTipoVeiculoId { get; set; }

    public string Status { get; set; }

    public string StatusDescricao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataEmissao { get; set; }

    public int ClienteId { get; set; }

    public string Cliente { get; set; }

    public int DepositoId { get; set; }

    public string Deposito { get; set; }

    public string Empresa { get; set; }

    public string Cnpj { get; set; }
}

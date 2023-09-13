using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepGrvAptoEmissaoNfe
{
    public int ClienteId { get; set; }

    public int DepositoId { get; set; }

    public int EmpresaId { get; set; }

    public int GrvId { get; set; }

    public int AtendimentoId { get; set; }

    public int FaturamentoId { get; set; }

    public string Cliente { get; set; }

    public string Deposito { get; set; }

    public string Empresa { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string InscricaoMunicipal { get; set; }

    public string InscricaoEstadual { get; set; }

    public string Token { get; set; }

    public DateTime DataLiberacao { get; set; }
}

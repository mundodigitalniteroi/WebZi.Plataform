using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepAlterdataTituloReceber
{
    public int ClienteId { get; set; }

    public int DepositoId { get; set; }

    public int ClienteDepositoId { get; set; }

    public int GrvId { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public int AtendimentoId { get; set; }

    public int FaturamentoId { get; set; }

    public int NfeId { get; set; }

    public int AlterDataDocumentoId { get; set; }

    public int AlterDataConfiguracaoId { get; set; }

    public string CodigoEmpresa { get; set; }

    public string IdentificadorPessoa { get; set; }

    public DateTime DataVencimento { get; set; }
}

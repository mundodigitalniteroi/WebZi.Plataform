using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepAlterdataLoteBaixa
{
    public int AlterDataTituloReceberId { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string IdentificadorTituloAreceber { get; set; }

    public string IdentificadorFormaPagamento { get; set; }

    public string IdentificadorContaBancaria { get; set; }

    public DateTime DataVencimento { get; set; }

    public string CodigoEmpresa { get; set; }

    public string LoteBaixaEnviado { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataDeBaixa { get; set; }
}

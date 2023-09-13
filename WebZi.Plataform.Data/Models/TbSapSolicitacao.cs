using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbSapSolicitacao
{
    public int IdSapSolicitacao { get; set; }

    public int IdTransacaoSap { get; set; }

    public string Operacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public int? IdGrv { get; set; }

    public int? IdAtendimento { get; set; }
}

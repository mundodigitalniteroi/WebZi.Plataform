using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataErro
{
    public int AlterDataErroId { get; set; }

    public int GrvOrigemId { get; set; }

    public string JsonEnvio { get; set; }

    public string CodigoErro { get; set; }

    public string MensagemErro { get; set; }

    public DateTime DataCadastro { get; set; }

    public int? TabelaOrigemErroId { get; set; }

    public string MetodoOrigemErro { get; set; }

    public string JsonRetorno { get; set; }

    public virtual TbDepGrv GrvOrigem { get; set; }
}

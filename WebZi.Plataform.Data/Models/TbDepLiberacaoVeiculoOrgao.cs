using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepLiberacaoVeiculoOrgao
{
    public int IdLiberacaoVeiculoOrgao { get; set; }

    public int IdGrv { get; set; }

    public string JsonEnvio { get; set; }

    public string JsonRetorno { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Status { get; set; }
}

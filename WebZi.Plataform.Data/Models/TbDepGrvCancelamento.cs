using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvCancelamento
{
    public int IdCancelamento { get; set; }

    public int IdGrv { get; set; }

    public byte IdTipoCancelamentoGrv { get; set; }

    public int IdUsuario { get; set; }

    public string MotivoCancelamento { get; set; }

    public string MatriculaSolicitante { get; set; }

    public string NomeSolicitante { get; set; }

    public DateTime DataCadastro { get; set; }
}

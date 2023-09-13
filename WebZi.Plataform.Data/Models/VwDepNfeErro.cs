using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepNfeErro
{
    public int GrvId { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string Cliente { get; set; }

    public string Deposito { get; set; }

    public int? IdentificadorNota { get; set; }

    public string OrigemErro { get; set; }

    public string Status { get; set; }

    public string CodigoErro { get; set; }

    public string MensagemErro { get; set; }

    public string CorrecaoErro { get; set; }

    public DateTime DataHoraCadastro { get; set; }
}

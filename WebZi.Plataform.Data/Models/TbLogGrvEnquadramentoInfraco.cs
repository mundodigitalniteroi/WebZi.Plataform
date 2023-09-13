using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogGrvEnquadramentoInfraco
{
    public int Id { get; set; }

    public int IdGrvEnquadramentoInfracao { get; set; }

    public int IdGrv { get; set; }

    public decimal IdEnquadramentoInfracao { get; set; }

    public int IdUsuario { get; set; }

    public string NumeroInfracao { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Crud { get; set; }
}

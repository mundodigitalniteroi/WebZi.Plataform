using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDeclaracaoAcesso
{
    public int IdDeclaracaoAcesso { get; set; }

    public string IdGrv { get; set; }

    public string Responsavel { get; set; }

    public string Rg { get; set; }

    public string Cpf { get; set; }

    public string Observacao { get; set; }

    public int IdUsuario { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime DataDeclaracao { get; set; }

    public int? IdPropositoAcesso { get; set; }
}

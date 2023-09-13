using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepPropositoAcessoDeclaracao
{
    public int Id { get; set; }

    public int IdPropositoAcesso { get; set; }

    public int IdDeclaracaoAcesso { get; set; }
}

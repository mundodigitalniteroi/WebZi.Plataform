using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSistemaPerfilAcessoSubModulo
{
    public int IdPerfilAcessoSubModulo { get; set; }

    public int IdPerfilAcesso { get; set; }

    public short IdSubModulo { get; set; }

    public string Crud { get; set; }

    public virtual TbDepSistemaPerfilAcesso IdPerfilAcessoNavigation { get; set; }

    public virtual TbDepSistemaSubModulo IdSubModuloNavigation { get; set; }
}

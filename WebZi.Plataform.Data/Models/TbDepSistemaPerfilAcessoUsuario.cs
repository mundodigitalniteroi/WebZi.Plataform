using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSistemaPerfilAcessoUsuario
{
    public int IdPerfilAcessoUsuario { get; set; }

    public int IdPerfilAcesso { get; set; }

    public int IdUsuario { get; set; }

    public virtual TbDepSistemaPerfilAcesso IdPerfilAcessoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }
}

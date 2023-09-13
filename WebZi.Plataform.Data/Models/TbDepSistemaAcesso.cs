using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSistemaAcesso
{
    public decimal IdAcesso { get; set; }

    public short IdModulo { get; set; }

    public short IdSubModulo { get; set; }

    public int IdUsuarioAcesso { get; set; }

    public string TipoAcesso { get; set; }

    public int IdUsuario { get; set; }

    public virtual TbDepSistemaModulo IdModuloNavigation { get; set; }

    public virtual TbDepSistemaSubModulo IdSubModuloNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAcessoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }
}

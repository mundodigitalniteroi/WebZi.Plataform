using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepPerfilAcessoUsuario
{
    public int IdUsuario { get; set; }

    public int IdPerfilAcesso { get; set; }

    public string PerfilAcessoDescricao { get; set; }

    public short IdModulo { get; set; }

    public string ModulosDescricao { get; set; }

    public string ModulosMenu { get; set; }

    public short ModulosOrdenacao { get; set; }

    public short IdSubModulo { get; set; }

    public string SubModuloMenu { get; set; }

    public string SubModuloFormulario { get; set; }

    public string SubModuloDescricao { get; set; }

    public short? SubModuloOrdenacao { get; set; }

    public string TipoAcesso { get; set; }
}

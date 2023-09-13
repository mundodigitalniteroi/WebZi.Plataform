using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepPerfilAcessoSubModulo
{
    public int IdPerfilAcessoSubModulo { get; set; }

    public int IdPerfilAcesso { get; set; }

    public short IdModulo { get; set; }

    public short IdSubModulo { get; set; }

    public string Crud { get; set; }

    public string PerfilAcessoDescricao { get; set; }

    public string PerfilAcessoFlagAtivo { get; set; }

    public string ModulosDescricao { get; set; }

    public string SubModulosDescricao { get; set; }

    public short ModulosOrdenacao { get; set; }

    public short? SubModulosOrdenacao { get; set; }

    public string SubModulosFlagAtivo { get; set; }
}

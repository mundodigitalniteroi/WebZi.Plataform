using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSistemaSubModulo
{
    public short IdSubModulo { get; set; }

    public short IdModulo { get; set; }

    public string Menu { get; set; }

    public string Formulario { get; set; }

    public string Descricao { get; set; }

    public byte[] Icone { get; set; }

    public string Status { get; set; }

    public short? Ordenacao { get; set; }

    public virtual TbDepSistemaModulo IdModuloNavigation { get; set; }

    public virtual ICollection<TbDepSistemaAcesso> TbDepSistemaAcessos { get; set; } = new List<TbDepSistemaAcesso>();

    public virtual ICollection<TbDepSistemaPerfilAcessoSubModulo> TbDepSistemaPerfilAcessoSubModulos { get; set; } = new List<TbDepSistemaPerfilAcessoSubModulo>();
}

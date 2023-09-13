using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSistemaModulo
{
    public short IdModulo { get; set; }

    public string Descricao { get; set; }

    public short Ordenacao { get; set; }

    public string Menu { get; set; }

    public virtual ICollection<TbDepSistemaAcesso> TbDepSistemaAcessos { get; set; } = new List<TbDepSistemaAcesso>();

    public virtual ICollection<TbDepSistemaSubModulo> TbDepSistemaSubModulos { get; set; } = new List<TbDepSistemaSubModulo>();
}

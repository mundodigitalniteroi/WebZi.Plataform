using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepStatusOperaco
{
    public string IdStatusOperacao { get; set; }

    public string Descricao { get; set; }

    public byte? Sequencia { get; set; }

    public string FlagVeiculoApreendido { get; set; }

    public string FlagLeilao { get; set; }

    public virtual ICollection<TbDepGrvBloqueio> TbDepGrvBloqueios { get; set; } = new List<TbDepGrvBloqueio>();

    public virtual ICollection<TbDepGrv> TbDepGrvs { get; set; } = new List<TbDepGrv>();
}

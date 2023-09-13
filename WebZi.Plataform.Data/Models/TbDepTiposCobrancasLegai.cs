using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepTiposCobrancasLegai
{
    public byte IdTipoCobrancaLegal { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepGrvCobrancasLegai> TbDepGrvCobrancasLegais { get; set; } = new List<TbDepGrvCobrancasLegai>();
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoRegrasPrestacaoConta
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbLeilao> TbLeilaos { get; set; } = new List<TbLeilao>();
}

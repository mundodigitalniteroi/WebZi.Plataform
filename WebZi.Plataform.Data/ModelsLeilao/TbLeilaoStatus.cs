using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoStatus
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public string ExibeMensagemConferencia { get; set; }

    public int? Sequencia { get; set; }

    public string Ativo { get; set; }

    public virtual ICollection<TbLeilao> TbLeilaos { get; set; } = new List<TbLeilao>();
}

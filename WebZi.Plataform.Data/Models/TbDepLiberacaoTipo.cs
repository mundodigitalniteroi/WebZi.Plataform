using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepLiberacaoTipo
{
    public byte IdLiberacaoTipo { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepLiberacao> TbDepLiberacaos { get; set; } = new List<TbDepLiberacao>();
}

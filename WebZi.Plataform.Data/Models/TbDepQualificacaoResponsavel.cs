using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepQualificacaoResponsavel
{
    public byte IdQualificacaoResponsavel { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepAtendimento> TbDepAtendimentos { get; set; } = new List<TbDepAtendimento>();
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAtendimentoFotosResponsavei
{
    public int IdAtendimentoFotoResponsavel { get; set; }

    public int IdAtendimento { get; set; }

    public byte[] Foto { get; set; }

    public virtual TbDepAtendimento IdAtendimentoNavigation { get; set; }
}

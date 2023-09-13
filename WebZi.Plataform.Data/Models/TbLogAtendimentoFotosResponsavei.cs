using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogAtendimentoFotosResponsavei
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int? IdAtendimentoFotoResponsavel { get; set; }

    public int? IdAtendimento { get; set; }

    public byte[] Foto { get; set; }
}

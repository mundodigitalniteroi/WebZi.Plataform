using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepLogSuporte
{
    public int IdSuporte { get; set; }

    public int IdUsuario { get; set; }

    public DateTime DataAcesso { get; set; }

    public int? IdGrv { get; set; }

    public int? IdFaturamento { get; set; }

    public int? IdAtendimento { get; set; }

    public string Tipo { get; set; }

    public string Ip { get; set; }
}

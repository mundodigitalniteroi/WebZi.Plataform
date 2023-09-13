using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogWebpatiosAcesso
{
    public long Id { get; set; }

    public DateTime DataHoraVisita { get; set; }

    public int IdUsuario { get; set; }

    public string IpUsuario { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogGeracaoRelatorio
{
    public short Id { get; set; }

    public int IdTipoRelatorio { get; set; }

    public int IdUsuario { get; set; }

    public DateTime Data { get; set; }

    public string Parametros { get; set; }

    public string Extracao { get; set; }
}

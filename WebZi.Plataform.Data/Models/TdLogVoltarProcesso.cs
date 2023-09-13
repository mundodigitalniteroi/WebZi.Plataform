using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TdLogVoltarProcesso
{
    public int Id { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public DateTime Data { get; set; }

    public string Ip { get; set; }
}

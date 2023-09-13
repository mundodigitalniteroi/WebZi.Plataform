using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepUsuariosCliente
{
    public long IdUsuarioCliente { get; set; }

    public int IdUsuario { get; set; }

    public short IdCliente { get; set; }

    public string Nome { get; set; }

    public string FlagAtivo { get; set; }
}

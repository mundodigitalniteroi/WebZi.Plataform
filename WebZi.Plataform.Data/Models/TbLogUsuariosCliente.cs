using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogUsuariosCliente
{
    public long Id { get; set; }

    public long? IdUsuarioCliente { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdCliente { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public DateTime? DataCadastro { get; set; }
}

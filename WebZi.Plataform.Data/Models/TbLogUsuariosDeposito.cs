using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogUsuariosDeposito
{
    public long Id { get; set; }

    public long? IdUsuarioDeposito { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdDeposito { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public DateTime? DataCadastro { get; set; }
}

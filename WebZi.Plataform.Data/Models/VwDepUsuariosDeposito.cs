using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepUsuariosDeposito
{
    public long IdUsuarioDeposito { get; set; }

    public int IdUsuario { get; set; }

    public short IdDeposito { get; set; }

    public string Descricao { get; set; }

    public string FlagAtivo { get; set; }
}

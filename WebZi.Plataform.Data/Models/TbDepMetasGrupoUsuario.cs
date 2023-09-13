using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepMetasGrupoUsuario
{
    public int IdDepMetasGrupoUsuarios { get; set; }

    public int IdDepMetasGrupo { get; set; }

    public int IdUsuario { get; set; }
}

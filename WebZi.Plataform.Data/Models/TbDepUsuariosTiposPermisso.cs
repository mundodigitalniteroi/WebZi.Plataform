using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepUsuariosTiposPermisso
{
    public short IdTipoPermissao { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepUsuariosPermisso> TbDepUsuariosPermissos { get; set; } = new List<TbDepUsuariosPermisso>();
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogAcesso
{
    public int IdUsuario { get; set; }

    public DateTime DataHoraAcesso { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }
}

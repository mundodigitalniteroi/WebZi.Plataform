using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepUsuariosCliente
{
    public long IdUsuarioCliente { get; set; }

    public int IdUsuario { get; set; }

    public int IdCliente { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepCliente IdClienteNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }
}

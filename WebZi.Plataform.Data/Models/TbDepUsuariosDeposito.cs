using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepUsuariosDeposito
{
    public long IdUsuarioDeposito { get; set; }

    public int IdUsuario { get; set; }

    public int IdDeposito { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepDeposito IdDepositoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }
}

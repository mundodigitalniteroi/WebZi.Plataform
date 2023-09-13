using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepComunicacaoEmail
{
    public int IdComunicacaoEmail { get; set; }

    public int IdClienteDeposito { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepClientesDeposito IdClienteDepositoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }
}

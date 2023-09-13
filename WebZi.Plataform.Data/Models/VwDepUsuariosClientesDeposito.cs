using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepUsuariosClientesDeposito
{
    public long IdUsuarioCliente { get; set; }

    public long IdUsuarioDeposito { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int IdUsuario { get; set; }

    public long? PessoaId { get; set; }

    public string Login { get; set; }

    public string Matricula { get; set; }

    public string Senha1 { get; set; }

    public string UsuarioFlagAtivo { get; set; }

    public string ClienteNome { get; set; }

    public string ClienteFlagAtivo { get; set; }

    public string DepositoNome { get; set; }

    public string DepositoFlagAtivo { get; set; }
}

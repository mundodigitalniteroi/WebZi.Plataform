using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepUsuariosWa
{
    public int IdUsuario { get; set; }

    public string Nome { get; set; }

    public string Cpf { get; set; }

    public string Email { get; set; }

    public DateTime? DataNascimento { get; set; }

    public string Senha { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataConfirmacao { get; set; }
}

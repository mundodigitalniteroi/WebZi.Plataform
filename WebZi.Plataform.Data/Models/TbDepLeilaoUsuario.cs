using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepLeilaoUsuario
{
    public int IdLeilaoUsuario { get; set; }

    public string Login { get; set; }

    public string Email { get; set; }

    public string Senha { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataDesativacao { get; set; }

    public string Status { get; set; }
}

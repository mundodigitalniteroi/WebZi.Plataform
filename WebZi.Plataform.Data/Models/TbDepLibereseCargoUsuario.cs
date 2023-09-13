using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepLibereseCargoUsuario
{
    public int IdLibereseCargo { get; set; }

    public int IdUsuario { get; set; }

    public string Cargo { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Nome { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepEnderecoEntrega
{
    public int IdEnderecoEntrega { get; set; }

    public int IdEndereco { get; set; }

    public int IdAtendimento { get; set; }

    public string Login { get; set; }

    public DateTime DataCadastro { get; set; }
}

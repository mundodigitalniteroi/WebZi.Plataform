using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepPropositoAcesso
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public DateTime DataCadastro { get; set; }

    public string IdUsuario { get; set; }
}

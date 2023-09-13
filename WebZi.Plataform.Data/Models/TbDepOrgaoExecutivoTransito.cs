using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepOrgaoExecutivoTransito
{
    public int IdOrgaoExecutivoTransito { get; set; }

    public string Descricao { get; set; }

    public string Uf { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual ICollection<TbDepCliente> TbDepClientes { get; set; } = new List<TbDepCliente>();
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepConfiguracoesNomeTabelaOrigem
{
    public short NomeTabelaOrigemId { get; set; }

    public string Codigo { get; set; }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public string DiretorioRemoto { get; set; }

    public virtual ICollection<TbDepRepositorioArquivo> TbDepRepositorioArquivos { get; set; } = new List<TbDepRepositorioArquivo>();
}

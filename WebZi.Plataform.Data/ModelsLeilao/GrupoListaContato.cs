using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class GrupoListaContato
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public DateTime DataHoraCadastro { get; set; }

    public string FlagAtivo { get; set; }
}

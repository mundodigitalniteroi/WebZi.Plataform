using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwTipoVeiculoClassificacao
{
    public byte IdTipoVeiculo { get; set; }

    public string Descricao { get; set; }

    public byte IdTipoVeiculoClassificacaoNome { get; set; }

    public string ClassificacaoNome { get; set; }
}

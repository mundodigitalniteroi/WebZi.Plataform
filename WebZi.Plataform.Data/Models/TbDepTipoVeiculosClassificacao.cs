using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepTipoVeiculosClassificacao
{
    public byte IdTipoVeiculoClassificacao { get; set; }

    public byte IdTipoVeiculoClassificacaoNome { get; set; }

    public byte IdTipoVeiculo { get; set; }

    public virtual TbDepTipoVeiculosClassificacaoNome IdTipoVeiculoClassificacaoNomeNavigation { get; set; }

    public virtual TbDepTipoVeiculo IdTipoVeiculoNavigation { get; set; }
}

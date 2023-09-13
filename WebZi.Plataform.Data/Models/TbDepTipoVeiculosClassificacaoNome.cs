using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepTipoVeiculosClassificacaoNome
{
    public byte IdTipoVeiculoClassificacaoNome { get; set; }

    public string Descricao { get; set; }

    public string Classificacao { get; set; }

    public virtual ICollection<TbDepReboquesTerceirizadosTarifa> TbDepReboquesTerceirizadosTarifas { get; set; } = new List<TbDepReboquesTerceirizadosTarifa>();

    public virtual ICollection<TbDepTipoVeiculosClassificacao> TbDepTipoVeiculosClassificacaos { get; set; } = new List<TbDepTipoVeiculosClassificacao>();
}

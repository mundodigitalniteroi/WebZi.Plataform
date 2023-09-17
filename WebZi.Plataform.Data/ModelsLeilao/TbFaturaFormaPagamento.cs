using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbFaturaFormaPagamento
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbArrematantesFatura> TbArrematantesFaturas { get; set; } = new List<TbArrematantesFatura>();

    public virtual ICollection<TbFaturaComposicaoCobranca> TbFaturaComposicaoCobrancas { get; set; } = new List<TbFaturaComposicaoCobranca>();
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataConfiguracaoIdentificadorNaturezaLancamento
{
    public short IdentificadorNaturezaLancamentoId { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbDepAlterdataConfiguracao> TbDepAlterdataConfiguracaos { get; set; } = new List<TbDepAlterdataConfiguracao>();
}

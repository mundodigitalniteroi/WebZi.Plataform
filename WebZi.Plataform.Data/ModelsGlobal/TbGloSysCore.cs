using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloSysCore
{
    public int IdCor { get; set; }

    public string Descricao { get; set; }

    public string DescricaoSecundaria { get; set; }

    public string FlagCorPrincipal { get; set; }

    public string FlagAtivo { get; set; }
}

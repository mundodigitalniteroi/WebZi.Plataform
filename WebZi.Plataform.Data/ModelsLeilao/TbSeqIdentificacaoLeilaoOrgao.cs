using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbSeqIdentificacaoLeilaoOrgao
{
    public int Id { get; set; }

    public string CodigoOrgao { get; set; }

    public string Ano { get; set; }

    public int? TipoLeilao { get; set; }

    public int? Seq { get; set; }

    public int? SeqMin { get; set; }

    public int? SeqMax { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogFaturamentoComposicaoNf
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int? IdFaturamentoComposicaoNf { get; set; }

    public int? IdFaturamentoComposicao { get; set; }

    public string Nota { get; set; }

    public DateTime? DataEmissaoNota { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepNfeFaturamentoComposicaoAgrupado
{
    public string NumeroFormularioGrv { get; set; }

    public int GrvId { get; set; }

    public int AtendimentoId { get; set; }

    public int CnaeId { get; set; }

    public string Cnae { get; set; }

    public int ListaServicoId { get; set; }

    public string Servico { get; set; }

    public string FlagEnviarValorIss { get; set; }

    public string FlagEnviarInscricaoEstadual { get; set; }

    public decimal? TotalComDesconto { get; set; }
}

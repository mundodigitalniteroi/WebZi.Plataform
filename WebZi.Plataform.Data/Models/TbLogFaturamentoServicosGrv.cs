using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogFaturamentoServicosGrv
{
    public int Id { get; set; }

    public int IdFaturamentoServicoGrv { get; set; }

    public int IdGrv { get; set; }

    public int IdFaturamentoServicoTipoVeiculo { get; set; }

    public int? IdUsuarioDesconto { get; set; }

    public int IdUsuario { get; set; }

    public decimal? Valor { get; set; }

    public string TempoTrabalhado { get; set; }

    public string OrigemCadastro { get; set; }

    public string TipoDesconto { get; set; }

    public int? QuantidadeDesconto { get; set; }

    public decimal? ValorDesconto { get; set; }

    public string ObservacaoDesconto { get; set; }

    public string FlagRealizarCobranca { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Crud { get; set; }
}

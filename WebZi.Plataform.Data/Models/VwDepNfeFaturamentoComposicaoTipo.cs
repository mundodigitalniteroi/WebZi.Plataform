using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepNfeFaturamentoComposicaoTipo
{
    public int FaturamentoId { get; set; }

    public int FaturamentoServicoTipoId { get; set; }

    public int FaturamentoServicoAssociadoId { get; set; }

    public int ClienteId { get; set; }

    public int DepositoId { get; set; }

    public int FaturamentoServicoTipoVeiculoId { get; set; }

    public byte TipoVeiculoId { get; set; }

    public int FaturamentoComposicaoId { get; set; }

    public string TipoServicoDescricao { get; set; }

    public string ServicoAssociadoDescricao { get; set; }

    public string TipoCobranca { get; set; }

    public string ServicoTipoCobrancaDescricao { get; set; }

    public DateTime ServicoAssociadoDataVigenciaInicial { get; set; }

    public DateTime? ServicoAssociadoDataVigenciaFinal { get; set; }

    public decimal ValorTipoComposicao { get; set; }

    public decimal? QuantidadeCobranca { get; set; }

    public decimal ValorComposicao { get; set; }

    public string TipoDesconto { get; set; }

    public int? QuantidadeDesconto { get; set; }

    public decimal? ValorDesconto { get; set; }

    public decimal ValorFaturado { get; set; }

    public decimal? QuantidadeAlterada { get; set; }

    public string CnaeCodigo { get; set; }

    public string CnaeDescricao { get; set; }

    public string ListaServicoItemLista { get; set; }

    public string ListaServicoAliquotaIss { get; set; }

    public string ListaServicoDescricao { get; set; }
}
